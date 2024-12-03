using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    //Esta variable almacena la rotaci�n inicial del arma
    private Quaternion startRotation;

    //Variable que almacenar� la cantidad de efecto o influencia de movimiento que le daremos al arma
    public float swayAmount = 8;


    // Start is called before the first frame update
    void Start()
    {
        //Esta l�nea almacena la rotaci�n inicial del arma al inicio del juego
        startRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        Sway();
    }

    //Este m�todo ser� el encargado de darle el efecto o influencia de movimiento a nuestra arma
    private void Sway()
    {
        //Variable que guarda el movimiento en x del rat�n. 
        float mouseX = Input.GetAxis("Mouse X");

        //Variable que guarda el movimiento en y del rat�n.
        float mouseY = Input.GetAxis("Mouse Y");

        //Esta variable almacenar� el grado de rotaci�n en "x" que sufrir� nuestro arma con el movimiento
        Quaternion xAngle = Quaternion.AngleAxis(mouseX * -1.25f, Vector3.up);

        //Esta variable almacenar� el grado de rotaci�n en "y" que sufrir� nuestro arma con el movimiento
        Quaternion yAngle = Quaternion.AngleAxis(mouseY * 1.25f, Vector3.left);

        //Esta variable almacena la rotaci�n final del arma tras sufir las rotaciones en "x" e "y" al final de cada paso del personaje
        Quaternion targetRotation = startRotation * xAngle * yAngle;

        //Esta l�nea hace que el arma efectue la rotaci�n final
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * swayAmount);
    }
}
