using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    //Array que almacenar� las armas que tenga el player
    public GameObject[] weapons;

    //Variable que almacenar� la posici�n en el array del arma seleccionada
    public int selectedWeapon = 0;


    // Start is called before the first frame update
    void Start()
    {
        //M�todo que activa el arma que se ha seleccionado
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        //Variable que almacena la posici�n del anterior arma, que en un principio es igual al arma seleccionada
        int previousWeapon = selectedWeapon;
        /*Si hacemos girar la rueda del rat�n hacia arriba, cambiar� al siguiente arma del array, pero si ya ten�amos seleccionado el ultimo
          arma, el arma seleccionado ser� el de la primera posici�n del array*/
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

        /*Si hacemos girar la rueda del rat�n hacia abajo, cambiar� al arma anterior del array, pero si ya ten�amos seleccionado el primer
         arma, el arma seleccionado ser� el de la ultima posici�n del array*/
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

        //Estos 3 condicionales controlan que la selecci�n del arma se realice con las teclas "1", "2" y "3" del teclado en vez de con la rueda
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

        /*Si el arma que ten�amos es diferente al arma seleccionada, se llama al m�todo "SelectWeapon()" que activar� el nuevo arma,
          y desactivar� el anterior*/
        if (previousWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    
    void SelectWeapon()
    {
        int i = 0;

        /*Por cada arma que tenga como hijo la "MainCamera" (la cual tiene asignado este script), siempre que su layer sea "Weapon", si este arma
          es igual al arma seleccionado se activar� y sino, se desactivar�*/
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
