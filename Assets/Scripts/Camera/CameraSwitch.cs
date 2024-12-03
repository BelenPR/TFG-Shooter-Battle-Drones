using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    //Variable que almacenar� el objeto c�mara en tercera persona o "ThirdPersonCamera"
    public Camera thirdPersonCamera;

    //Variable que almacenar� el objeto c�mara en primera persona o "MainCamera"
    public Camera firstPersonCamera;

    //Variable booleana que controla si est� o no activa la c�mara en primera persona, que en un primer momento, s� lo estar�
    private bool firstPersonEnabled = true;

    //Variable que controla si est� desactivada la visualizaci�n del personaje principal
    public bool disableMeshPlayerInFirstPerson = true;

    //Variable que almacena la malla de visualizaci�n del personaje principal
    public SkinnedMeshRenderer meshPlayer;

    //Cambio de vista del arma

    //Array que almacenar� las posiciones de los diferentes armas cuando la c�mara est� en primera persona
    public Transform[] weaponsTransformsFirstPerson;
    //Array que almacenar� las posiciones de los diferentes armas cuando la c�mara est� en tercera persona
    public Transform[] weaponsTransformsThirdPerson;
    //Array que almacena las diferentes armas
    public GameObject[] weapons;

    private void Start()
    {
        //Al inicio del juego, que la c�mara estar� en primera persona,  estar� desactivada la malla de visualizaci�n del personaje
        if (disableMeshPlayerInFirstPerson)
        {
            meshPlayer.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Si pulsamos la tecla "T" se cambia de la camara en primera persona a la camara en tercera persona y vicebersa 
        if (Input.GetKeyDown(KeyCode.T))
        {
            firstPersonEnabled = !firstPersonEnabled;
            ChangeCamera();
        }
    }

    public void ChangeCamera()
    {
        /*Si la primera persona est� activada, se desactiva la malla de visualizaci�n del personaje,se activa el componente "Camera" de la 
         * "MainCamera" y se desactiva el de la "ThirdPersonCamera" y sino, se activa la malla de visualizaci�n del personaje, se desactiva el 
         * componente "Camera" de la "MainCamera" y se activa el de la "ThirdPersonCamera"*/
        if (firstPersonEnabled)
        {
            if (disableMeshPlayerInFirstPerson)
            {
                meshPlayer.enabled = false;
            }

            firstPersonCamera.enabled = true;
            thirdPersonCamera.enabled = false;

            //Funci�n que cambia las posiciones de las armas a las posiciones que tienen que tener las mismas cuando la c�mara est� en primera persona
            ChangeWeaponFirstPerson();
        }
        else
        {
            meshPlayer.enabled = true;

            firstPersonCamera.enabled = false;
            thirdPersonCamera.enabled = true;

            //Funci�n que cambia las posiciones de las armas a las posiciones que tienen que tener las mismas cuando la c�mara est� en tercera persona
            ChangeWeaponThirdPerson();
        }
    }

    public void ChangeWeaponFirstPerson()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].transform.position = weaponsTransformsFirstPerson[i].transform.position;
            weapons[i].transform.rotation = weaponsTransformsFirstPerson[i].transform.rotation;
            weapons[i].transform.localScale = weaponsTransformsFirstPerson[i].transform.localScale;
        }
    }

    public void ChangeWeaponThirdPerson()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].transform.position = weaponsTransformsThirdPerson[i].transform.position;
            weapons[i].transform.rotation = weaponsTransformsThirdPerson[i].transform.rotation;
            weapons[i].transform.localScale = weaponsTransformsThirdPerson[i].transform.localScale;
        }
    }
}
