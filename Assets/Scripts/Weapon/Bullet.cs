using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Este método será llamado cuando el objeto que posee este script entre en colisión con otro objeto y se le pasa por parámetro el objeto con el que ha colisionado
    private void OnCollisionEnter(Collision collision)
    {
        /*Este condicional controla que si el objeto con el que choca la bala tiene el "tag" o etiqueta de "Enemy" se llama a la función "LooseLife"
          de ese enemigo que le restará una vida al mismo*/
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<AI>().LoseLife(1);
        }

        /*Este condicional controla que si el objeto con el que choca la bala tiene el "tag" 
         * o etiqueta de "Fragile" se destrulle ese objeto*/
        if (collision.gameObject.CompareTag("Fragile"))
        {
            Destroy(collision.gameObject);
        }
    }
}
