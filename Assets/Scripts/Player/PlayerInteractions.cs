using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    //Variable que almacena la posici�n del personaje, para devolverlo a esta posici�n cuando se caiga del escenario
    public Transform startPosition;
    
    private void OnTriggerEnter(Collider other)
    {
        /*Este condicional controla que si el personaje choca con un objeto cuyo "tag" es "GunAmmo" se suma la cantidad de balas
          indicadas en el script de dicho objeto, a la munici�n que ya tubiese nuestro arma, y destruye dicho objeto.*/
        if (other.gameObject.CompareTag("GunAmmo"))
        {
            GameManager.Instance.gameAmmo += other.gameObject.GetComponent<AmmoBox>().ammo;
            Destroy(other.gameObject);
        }

        /*Este condicional controla que si el personaje choca con un objeto cuyo "tag" es "HealthObject" se suma la cantidad de vida
          indicadas en el script de dicho objeto, a la vida que ya tubiese nuestro player, y destruye dicho objeto.*/
        if (other.gameObject.CompareTag("HealthObject"))
        {
            GameManager.Instance.AddHealth(other.gameObject.GetComponent<HealthObject>().health);
            Destroy(other.gameObject);
        }

        /*Este condicional controla que si el personaje choca con un objeto cuyo "tag" es "DeathFloor", es decir, se cae del escenario
          se restan 50 puntos de vida, a la vida que ya ten�a el player y hacemos que �ste reaparezca en el escenario*/
        if (other.gameObject.CompareTag("DeathFloor"))
        {
            GameManager.Instance.LoseHealth(50);
            //Se debe inhabilitar el componente "CharacterController" para poder cambiar la posici�n del personaje
            GetComponent<CharacterController>().enabled = false;
            gameObject.transform.position = startPosition.position;
            //Despu�s de mover la posici�n del personaje, reactivamos su "CharacterController" para poder controlar al mismo
            GetComponent<CharacterController>().enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*Condicional que controla que si el player colisiona con un objeto con el "tag" de "EnemyBullet" (con una bala enemiga)
          se restan 5 puntos de vida a la vida que ya ten�a el player*/
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            GameManager.Instance.LoseHealth(5);
        }
    }
}
