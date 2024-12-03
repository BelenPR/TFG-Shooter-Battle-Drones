using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    //Variable que controla la sensibilidad de movimiento del ratón
    public float mouseSensitivity = 80f;

    //Variable que almacena el componente "Transform" del jugador, es decir, la referencia de posición y rotación del mismo
    public Transform playerBody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //Con esta línea de código limitamos el movimiento del ratón a la pantalla en la que se desarrolla el juego
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Variable que guarda el movimiento en x del ratón. "Mouse X" es un nombre predeterminado por unity para hacer referencia al movimiento en X del ratón
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        //Variable que guarda el movimiento en y del ratón. "Mouse Y" es un nombre predeterminado por unity para hacer referencia al movimiento en X del ratón
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Con esta línea indicamos que la rotación sobre el eje x comienza en 0 y aumenta o disminuye con el movimiento en Y del ratón
        xRotation -= mouseY;

        //Con esta modificación limitamos la rotación sobre el eje X para que solo pueda realizarse en angulo de 90 arriba o abajo, impidiendo que la cabeza del personaje de una vuelta completa
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Esta línea cambia la rotación del personaje sobre el eje X (Vector3.up) cuando movemos el ratón arriba o abajo.
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        //Esta línea cambia la rotación del personaje sobre el eje Y (Vector3.up) cuando movemos el ratón a izquierda y derecha. 
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
