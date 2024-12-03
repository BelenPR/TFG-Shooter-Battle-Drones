using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    //Variable que almacena el tiempo que tardará en explotar la granada
    public float delay = 3f;

    //Variable contador que se encargará de la cuenta atrás para la detonación
    float countDown;

    //Variable que almacena el radio de afectación o daño de la granada
    public float radius = 5f;

    //Variable que almacena la fuerza de explosión
    public float explosionForce = 70f;

    //Variable que comprueba si ha explotado la granada
    bool exploded = false;

    //Variable que almacena el sistema de partículas que hace el efecto de la explosión
    public GameObject explosionEffect;

    //Variable que almacenará el componente "Audio Source" encargado de la emisión del sonido de la granada al explotar
    private AudioSource audioSource;

    //Variable que almacenará el sonido que emitirá el componente "Audio Source" al explotar la granada
    public AudioClip explosionSound;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //Al inicio del juego quedarán 3 segundos para que explote la granada porque auno no se ha lanzado
        countDown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        //La cuenta atrás para que explote la granada disminuye conforme pasa el tiempo
        countDown -= Time.deltaTime;

        if(countDown < 0 && exploded == false)
        {
            Explode();
            exploded = true;
        }
    }

    void Explode()
    {
        //Esta línea instancia el sistema de partículas cuando explota la granada
        Instantiate(explosionEffect, transform.position, transform.rotation);

        //Array que almacena todos los objetos con los que haya chocado el radio de afectación/daño de la granada
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        /*Por cada objeto almacenado en el array "colliders", se obtiene su script "AI" y si este no es null,.También se obtiene su "rigidbody"
          y si este no es null (el objeto tiene "rigidbody") se aplicará una fuerza de explosión contraria a la posición de la granada */
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

        //Línea que reproduce el sonido de explosión
        audioSource.PlayOneShot(explosionSound);

        //Líneas que desactivan la colisión y la vista de la granada antes de que se destruya
        gameObject.GetComponent<SphereCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        //Destruimos la granada 6 segundos después de que explote para dar tiempo a que se reproduzca el sonido de explosión
        Destroy(gameObject, delay*2);
    }
}
