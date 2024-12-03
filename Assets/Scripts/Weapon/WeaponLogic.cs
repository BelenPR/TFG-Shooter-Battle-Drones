using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLogic : MonoBehaviour
{
    //Esta variable almacenar� el punto desde el que se originan las balas, la punta de nuestra pistola
    public Transform spawnPoint;

    //Este ser� el objeto que instanciaremos, la bala
    public GameObject bullet;

    //Esta variable almacena la fuerza del disparo, la velocidad con que ir� despedida la bala
    public float shotForce = 1500f;

    //Esta variable almacenar� el tiempo de intervalo entre bala y bala
    public float shotRate = 0.5f;

    //Esta variable almacenar� el tiempo que ha transcurrido desde que se dispar� la ultima bala, para que cuando llegue a 0,5 pueda dispararse otra
    private float shotRateTime = 0f;

    //Variable que almacenar� el componente "Audio Source" encargado de la emisi�n del sonido del arma al disparar
    private AudioSource audioSource;

    //Variable que almacenar� el sonido que emitir� el componente "Audio Source" al disparar
    public AudioClip shotSound;

    //Variable que controlar� si nuestro arma es o no de disparo continuo
    public bool continueShooting = false;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        /*Este condicional se encarga de instanciar una bala desde un punto concreto cuando pulsemos el bot�n izquierdo del rat�n si el tiempo 
         * no est� parado o de dejar de disparar si levantamos el dedo del bot�n izquierdo del rat�n y nuestro arma ten�a disparo continuo*/
        if (Input.GetButtonDown("Fire1") && Time.timeScale != 0)
        {
            /*Con este condicional comprobamos el tiempo que ha pasado desde que empieza el juego, y si este tiempo es mayor que el ratio 
             * de tiempo entre balas, y adem�s hay munici�n (hay m�s de 0 balas), si el armana tiene disparo continuo, 
             * se llama continuamente al m�todo "Shoot", y sino, se le llama una vez. 
             */
           if(Time.time > shotRateTime && GameManager.Instance.gameAmmo > 0)
            {
                if (continueShooting)
                {
                    //M�todo que sirve para repetir la ejecuci�n de un m�todo, en una cantidad determinada de tiempo, cada ciertos segundos
                    InvokeRepeating("Shoot", 0.001f,shotRate);
                }
                else
                {
                    //M�todo que dispara una bala en una direccion y con una fuerza concreta, cada cierto tiempo y la elimina al trancurrir 5 segundos.
                    Shoot();
                }
            }
        }
        else if (Input.GetButtonUp("Fire1") && continueShooting && Time.timeScale != 0)
        {
            //M�todo que cancela la invocaci�n repetitiva de un m�todo iniciada en alg�n punto anterior del c�digo
            CancelInvoke("Shoot");
        }
    }

    public void Shoot()
    {
        if(GameManager.Instance.gameAmmo > 0)
        {
            //L�nea que reproduce el sonido de disparo
            audioSource.PlayOneShot(shotSound);
            //L�nea que resta uno a la cantidad de munici�n que ya ten�a el player
            GameManager.Instance.gameAmmo--;
            GameObject newBullet;
            newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce);
            shotRateTime = Time.time + shotRate;
            //Esta l�nea destruye la bala trancurridos 1 segundo para ahorrar recursos.
            Destroy(newBullet, 1);
        }
        else
        {
            CancelInvoke("Shoot");
        }
    }
}
