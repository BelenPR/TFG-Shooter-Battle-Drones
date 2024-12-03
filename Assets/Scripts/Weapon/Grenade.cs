using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    //Variable que almacena el tiempo que tardar� en explotar la granada
    public float delay = 3f;

    //Variable contador que se encargar� de la cuenta atr�s para la detonaci�n
    float countDown;

    //Variable que almacena el radio de afectaci�n o da�o de la granada
    public float radius = 5f;

    //Variable que almacena la fuerza de explosi�n
    public float explosionForce = 70f;

    //Variable que comprueba si ha explotado la granada
    bool exploded = false;

    //Variable que almacena el sistema de part�culas que hace el efecto de la explosi�n
    public GameObject explosionEffect;

    //Variable que almacenar� el componente "Audio Source" encargado de la emisi�n del sonido de la granada al explotar
    private AudioSource audioSource;

    //Variable que almacenar� el sonido que emitir� el componente "Audio Source" al explotar la granada
    public AudioClip explosionSound;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //Al inicio del juego quedar�n 3 segundos para que explote la granada porque auno no se ha lanzado
        countDown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        //La cuenta atr�s para que explote la granada disminuye conforme pasa el tiempo
        countDown -= Time.deltaTime;

        if(countDown < 0 && exploded == false)
        {
            Explode();
            exploded = true;
        }
    }

    void Explode()
    {
        //Esta l�nea instancia el sistema de part�culas cuando explota la granada
        Instantiate(explosionEffect, transform.position, transform.rotation);

        //Array que almacena todos los objetos con los que haya chocado el radio de afectaci�n/da�o de la granada
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        /*Por cada objeto almacenado en el array "colliders", se obtiene su script "AI" y si este no es null,.Tambi�n se obtiene su "rigidbody"
          y si este no es null (el objeto tiene "rigidbody") se aplicar� una fuerza de explosi�n contraria a la posici�n de la granada */
        foreach(var rangeObgects in colliders)
        {
            AI ai = rangeObgects.GetComponent<AI>();
            if(ai != null)
            {
                ai.GrenadeImpact();
            }

            Rigidbody rb = rangeObgects.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(explosionForce * 10, transform.position, radius);
            }
        }

        //L�nea que reproduce el sonido de explosi�n
        audioSource.PlayOneShot(explosionSound);

        //L�neas que desactivan la colisi�n y la vista de la granada antes de que se destruya
        gameObject.GetComponent<SphereCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        //Destruimos la granada 6 segundos despu�s de que explote para dar tiempo a que se reproduzca el sonido de explosi�n
        Destroy(gameObject, delay*2);
    }
}
