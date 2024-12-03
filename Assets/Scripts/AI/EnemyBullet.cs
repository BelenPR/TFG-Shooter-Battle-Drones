using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Destruye la bala trancurridos 3 segundos desde su creación
        Destroy(gameObject, 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Condicional que controla que si la bala choca contra el enemigo se destruya
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
