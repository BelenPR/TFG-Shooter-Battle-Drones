using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    //Variable que controla la sensibilidad de movimiento del rat�n
    public float mouseSensitivity = 80f;

    //Variable que almacena el componente "Transform" del jugador, es decir, la referencia de posici�n y rotaci�n del mismo
    public Transform playerBody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //Con esta l�nea de c�digo limitamos el movimiento del rat�n a la pantalla en la que se desarrolla el juego
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Variable que guarda el movimiento en x del rat�n. "Mouse X" es un nombre predeterminado por unity para hacer referencia al movimiento en X del rat�n
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        //Variable que guarda el movimiento en y del rat�n. "Mouse Y" es un nombre predeterminado por unity para hacer referencia al movimiento en X del rat�n
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Con esta l�nea indicamos que la rotaci�n sobre el eje x comienza en 0 y aumenta o disminuye con el movimiento en Y del rat�n
        xRotation -= mouseY;

        //Con esta modificaci�n limitamos la rotaci�n sobre el eje X para que solo pueda realizarse en angulo de 90 arriba o abajo, impidiendo que la cabeza del personaje de una vuelta completa
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Esta l�nea cambia la rotaci�n del personaje sobre el eje X (Vector3.up) cuando movemos el rat�n arriba o abajo.
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        //Esta l�nea cambia la rotaci�n del personaje sobre el eje Y (Vector3.up) cuando movemos el rat�n a izquierda y derecha. 
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
