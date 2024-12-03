using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    //Variable que almacena el objeto "StaminaSlider" o barra de stamina
    public Slider staminaSlider;

    //Variable que almacena el valor máximo de la barra de stamina
    public float maxStamina = 100f;

    //Variable que almacena la cantidad de stamina actual.
    private float currentStamina;

    //Variable que indica cada cuanto tiempo se regenera la barra de stamina
    private float regenerateStaminaTime = 0.1f;

    //Variable que almacena la cantidad de stamina que se va a regenerar por unidad de tiempo
    private float regenerateAmount = 2f;

    //Variable que indica cada cuanto tiempo se va perdiendo stamina
    private float losingStaminaTime = 0.1f;

    //
    private Coroutine myCoroutineLosing;
    private Coroutine myCoroutineRegenerate;


    // Start is called before the first frame update
    void Start()
    {
        //Al inicio del juego la cantidad actual de stamina estará al máximo y así lo indica la barra de stamina
        currentStamina = maxStamina;
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
    }

    public void UseStamina(float amount)
    {
        /*Condicional que controla que si la cantidad actual de stamina menos la cantidad de stamina que se pierde es mayor que 0,
          se va perdiendo stamina, y si por el contrario, ya es menor que 0, no se podrá perder más stamina porque ésta ya estará a 0*/
        if(currentStamina - amount > 0)
        {
            if( myCoroutineLosing != null)
            {
                StopCoroutine(myCoroutineLosing);
            }
            myCoroutineLosing = StartCoroutine(LosingStaminaCoroutine(amount));

            if (myCoroutineRegenerate != null)
            {
                StopCoroutine(myCoroutineRegenerate);
            }
            myCoroutineRegenerate = StartCoroutine(RegenerateStaminaCoroutine());
        }
        else
        {
            Debug.Log("No hay stamina");
            /*Linea que encuentra el objeto que contiene el script "PlayerMovement" (nuestro player) y pone su variable "isSprinting"
              a false para que deje de correr*/
            FindObjectOfType<PlayerMovement>().isSprinting = false;
        }
    }

    //Corrutina que controla la pérdida de stamina
    private IEnumerator LosingStaminaCoroutine(float amount)
    {
        //Mientras la stamina actual sea mayor o igual a 0 se pierde stamina y así se refleja visualmente en la barra de stamina
        while(currentStamina >= 0)
        {
            currentStamina -= amount;
            staminaSlider.value = currentStamina;
            yield return new WaitForSeconds(losingStaminaTime); 
        }

        myCoroutineLosing = null;

        /*Linea que encuentra el objeto que contiene el script "PlayerMovement" (nuestro player) y pone su variable "isSprinting"
          a false para que deje de correr*/
        FindObjectOfType<PlayerMovement>().isSprinting = false;
    }

    //Corrutina que controla la recuperación de stamina
    private IEnumerator RegenerateStaminaCoroutine()
    {
        yield return new WaitForSeconds(1);

        /*Mientras la stamina actual sea menor a la stamina máxima, se regenera poco a poco la stamina, y así se refleja visualmente
          enabled la barra de stamina*/
        while(currentStamina < maxStamina)
        {
            currentStamina += regenerateAmount;
            staminaSlider.value = currentStamina;
            yield return new WaitForSeconds(regenerateStaminaTime);
        }

        myCoroutineRegenerate = null;
    }

}
