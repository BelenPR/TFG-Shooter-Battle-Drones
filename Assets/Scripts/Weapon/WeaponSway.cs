using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    //Esta variable almacena la rotación inicial del arma
    private Quaternion startRotation;

    //Variable que almacenará la cantidad de efecto o influencia de movimiento que le daremos al arma
    public float swayAmount = 8;


    // Start is called before the first frame update
    void Start()
    {
        //Esta línea almacena la rotación inicial del arma al inicio del juego
        startRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        Sway();
    }

    //Este método será el encargado de darle el efecto o influencia de movimiento a nuestra arma
    private void Sway()
    {
        //Variable que guarda el movimiento en x del ratón. 
        float mouseX = Input.GetAxis("Mouse X");

        //Variable que guarda el movimiento en y del ratón.
        float mouseY = Input.GetAxis("Mouse Y");

        //Esta variable almacenará el grado de rotación en "x" que sufrirá nuestro arma con el movimiento
        Quaternion xAngle = Quaternion.AngleAxis(mouseX * -1.25f, Vector3.up);

        //Esta variable almacenará el grado de rotación en "y" que sufrirá nuestro arma con el movimiento
        Quaternion yAngle = Quaternion.AngleAxis(mouseY * 1.25f, Vector3.left);

        //Esta variable almacena la rotación final del arma tras sufir las rotaciones en "x" e "y" al final de cada paso del personaje
        Quaternion targetRotation = startRotation * xAngle * yAngle;

        //Esta línea hace que el arma efectue la rotación final
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * swayAmount);
    }
}
