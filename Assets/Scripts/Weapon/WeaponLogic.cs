using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLogic : MonoBehaviour
{
    //Esta variable almacenará el punto desde el que se originan las balas, la punta de nuestra pistola
    public Transform spawnPoint;

    //Este será el objeto que instanciaremos, la bala
    public GameObject bullet;

    //Esta variable almacena la fuerza del disparo, la velocidad con que irá despedida la bala
    public float shotForce = 1500f;

    //Esta variable almacenará el tiempo de intervalo entre bala y bala
    public float shotRate = 0.5f;

    //Esta variable almacenará el tiempo que ha transcurrido desde que se disparó la ultima bala, para que cuando llegue a 0,5 pueda dispararse otra
    private float shotRateTime = 0f;

    //Variable que almacenará el componente "Audio Source" encargado de la emisión del sonido del arma al disparar
    private AudioSource audioSource;

    //Variable que almacenará el sonido que emitirá el componente "Audio Source" al disparar
    public AudioClip shotSound;

    //Variable que controlará si nuestro arma es o no de disparo continuo
    public bool continueShooting = false;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        /*Este condicional se encarga de instanciar una bala desde un punto concreto cuando pulsemos el botón izquierdo del ratón si el tiempo 
         * no está parado o de dejar de disparar si levantamos el dedo del botón izquierdo del ratón y nuestro arma tenía disparo continuo*/
        if (Input.GetButtonDown("Fire1") && Time.timeScale != 0)
        {
            /*Con este condicional comprobamos el tiempo que ha pasado desde que empieza el juego, y si este tiempo es mayor que el ratio 
             * de tiempo entre balas, y además hay munición (hay más de 0 balas), si el armana tiene disparo continuo, 
             * se llama continuamente al método "Shoot", y sino, se le llama una vez. 
             */
           if(Time.time > shotRateTime && GameManager.Instance.gameAmmo > 0)
            {
                if (continueShooting)
                {
                    //Método que sirve para repetir la ejecución de un método, en una cantidad determinada de tiempo, cada ciertos segundos
                    InvokeRepeating("Shoot", 0.001f,shotRate);
                }
                else
                {
                    //Método que dispara una bala en una direccion y con una fuerza concreta, cada cierto tiempo y la elimina al trancurrir 5 segundos.
                    Shoot();
                }
            }
        }
        else if (Input.GetButtonUp("Fire1") && continueShooting && Time.timeScale != 0)
        {
            //Método que cancela la invocación repetitiva de un método iniciada en algún punto anterior del código
            CancelInvoke("Shoot");
        }
    }

    public void Shoot()
    {
        if(GameManager.Instance.gameAmmo > 0)
        {
            //Línea que reproduce el sonido de disparo
            audioSource.PlayOneShot(shotSound);
            //Línea que resta uno a la cantidad de munición que ya tenía el player
            GameManager.Instance.gameAmmo--;
            GameObject newBullet;
            newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce);
            shotRateTime = Time.time + shotRate;
            //Esta línea destruye la bala trancurridos 1 segundo para ahorrar recursos.
            Destroy(newBullet, 1);
        }
        else
        {
            CancelInvoke("Shoot");
        }
    }
}
