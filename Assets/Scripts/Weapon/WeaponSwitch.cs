using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    //Array que almacenará las armas que tenga el player
    public GameObject[] weapons;

    //Variable que almacenará la posición en el array del arma seleccionada
    public int selectedWeapon = 0;


    // Start is called before the first frame update
    void Start()
    {
        //Método que activa el arma que se ha seleccionado
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        //Variable que almacena la posición del anterior arma, que en un principio es igual al arma seleccionada
        int previousWeapon = selectedWeapon;
        /*Si hacemos girar la rueda del ratón hacia arriba, cambiará al siguiente arma del array, pero si ya teníamos seleccionado el ultimo
          arma, el arma seleccionado será el de la primera posición del array*/
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(selectedWeapon >= weapons.Length - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }

        /*Si hacemos girar la rueda del ratón hacia abajo, cambiará al arma anterior del array, pero si ya teníamos seleccionado el primer
         arma, el arma seleccionado será el de la ultima posición del array*/
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = weapons.Length - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        //Estos 3 condicionales controlan que la selección del arma se realice con las teclas "1", "2" y "3" del teclado en vez de con la rueda
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Length >=2)
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && weapons.Length >= 3)
        {
            selectedWeapon = 2;
        }

        /*Si el arma que teníamos es diferente al arma seleccionada, se llama al método "SelectWeapon()" que activará el nuevo arma,
          y desactivará el anterior*/
        if (previousWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    
    void SelectWeapon()
    {
        int i = 0;

        /*Por cada arma que tenga como hijo la "MainCamera" (la cual tiene asignado este script), siempre que su layer sea "Weapon", si este arma
          es igual al arma seleccionado se activará y sino, se desactivará*/
        foreach(Transform weapon in transform)
        {
            if(weapon.gameObject.layer == LayerMask.NameToLayer("Weapon"))
            {
                if(i == selectedWeapon)
                {
                    weapon.gameObject.SetActive(true);
                }
                else
                {
                    weapon.gameObject.SetActive(false);
                }

                i++;
            }
        }
    }
}
