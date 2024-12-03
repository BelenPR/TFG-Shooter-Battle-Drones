using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    //Variable que almacena la fuerza de lanzamiento de la granada
    public float throwForce = 500f;

    //Variable que almacena el objeto a lanzar (la granada)
    public GameObject grenadePrefab;

    // Update is called once per frame
    void Update()
    {
        //Este condicional se encarga de llamar al método que lanza la granada cuando se pulsa la tecla E del teclado y el tiempo no está parado 
        if (Input.GetKeyDown(KeyCode.E) && Time.timeScale != 0)
        {
            Throw();
        }
    }

    public void Throw()
    {
        //Esta línea instancia una granada en el punto donde se instanciaban las balas
        GameObject newGrenade = Instantiate(grenadePrefab, transform.position, transform.rotation);

        //Esta línea tira la granada instancada hacia delante con una fuerza determinada
        newGrenade.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce);
    }
}
