using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Esta variable almacena el componente CharacterControler del personaje que arrastraremos desde unity
    public CharacterController characterController;

    //Variable que indica la velocidad de movimiento del personaje
    public float speed = 10f;

    //Variable que almacena la gravedad que har� que el player caiga y no se mantenga flotando
    private float gravity = -9.81f;

    //Vector que indicar� la direcci�n con que el personaje caer� debido a la gravedad
    Vector3 velocity;

    //Esta variable almacena la posici�n de un objeto vac�o, hijo del player, que determinar� si este est� tocando tierra
    public Transform groundCheck;

    //Este ser� el radio de una esfera cuyo centro ser� el objeto vac�o y que servir� para detectar el suelo
    public float sphereRadius = 0.3f;

    //�sta variable almacena la m�scara del suelo
    public LayerMask groundMask;

    //Esta variable determianr� si el player est� o no tocando el suelo
    bool isGrounded;

    //Variable que determina la fuerza o altura de salto
    public float jumpHeight = 3f;

    //Variable que controla si estamos o no corriendo
    public bool isSprinting;

    //Variable que almacena el n�mero por el que multiplicaremos la velocidad de movimiento normal que ya tiene nuestro jugador para hacer que corra
    public float sprintingSpeedMultiplier = 1.5f;

    //Variable que almacena el n�mero por el que multiplicaremos la velocidad de movimiento normal que ya tiene nuestro jugador para hacer que no corra
    public float sprintSpeed = 1f;


    //Variable que almacena la cantidad de stamina que pierde el player cuando corre
    public float staminaUseAmount = 5f;

    //Variable que almacena el objeto "StaminaSlider" o barra de stamina
    private StaminaBar staminaSlider;

    //Variable que almacena el objeto "animator" que controlar� el cambio entre animaciones del personaje
    public Animator animator;


    private void Start()
    {
        staminaSlider = FindObjectOfType<StaminaBar>();
    }

    // Update is called once per frame
    void Update()
    {
        //Esta l�nea comprueba si la esfera que creamos, situada en el objeto vac�o est� tocando la m�scara del suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, sphereRadius, groundMask);

        //Si la esfera, es decir, el player est� tocando el suelo y su direcci�n en "y" es menor que 0, se establece el valor del vector en "y" a -2. 
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Estas l�neas almacenar�n el movimiento en los ejes "x" e "y" cuando se pulsena las teclas predeterminadas para "Horizontal" y "Vertical"
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        /*Estas l�neas asocian el movimiento en los ejes x y z con las variables de velocidad creadas en el animator para que cuando haya un cambio
         * de movimiento en estos ejes, tambi�n cambien las animaciones asignadas a cada uno de los ejes.
         */
        animator.SetFloat("VelX",x);
        animator.SetFloat("VelZ", z);

        /*Esta l�nea asocia el estado de correr (isSprinting) controlado en el c�digo con la variable booleana creada en el animator para que cuando
         * haya un cambio entre andar y correr del personaje, tambi�n cambien las animaciones asignadas a andar y correr.
         */
        animator.SetBool("isSprinting", isSprinting);

        //Este vector indica la direcci�n del movimiento del personaje, que ser� desde la posici�n en la que est� el personaje(transform) hacia la direcci�n indicadas al pulsar los botones indicados en las variables "x" y "z"
        Vector3 move = transform.right * x + transform.forward * z;

        //Se llama al m�todo "JumpCheck" para comprobar si se ha pulsado la tecla "espacio" para que el personaje salte
        JumpCheck();

        //Se llama al m�todo "RunCheck" para comprobar si se ha pulsado la tecla "shift" para que el personaje corra o deje de correr
        RunCheck();

        //Esta l�nea sirve para dar movimiento al personaje
        characterController.Move(move * speed * Time.deltaTime * sprintSpeed);

        //Esta l�nea indica la direcci�n en y del vector "velocity", que ser� la direcci�n que causa la gravedad en el personaje
        velocity.y += gravity * Time.deltaTime;

        //Esta l�nea har� que el personaje descienda debido a la gravedad
        characterController.Move(velocity * Time.deltaTime);
    }

    public void JumpCheck()
    {
        //Este condicional sirve para que si pulsamos el bot�n espacio del teclado mientras estamos en el suelo, nuestro player salte
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            animator.SetBool("isJumping", true);
        }

        if (!isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
    }

    public void RunCheck()
    {
        /*Condicional que controla que si pulsamos la tecla "Shift" y el personaje estaba corriendo, deja de correr
          y si no estaba corriendo, comienza a correr*/
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = !isSprinting;
            /*Condicional que controla que si el personaje est� corriendo pierde una cantidad de stamina, y sino, 
              no pierde stamina*/
            if (isSprinting)
            {
                staminaSlider.UseStamina(staminaUseAmount);
            }
            else
            {
                staminaSlider.UseStamina(0);
            }
        }

        /*Condicional que controla que si el personaje est� corriendo la velocidad del personaje se multiplica, y sino, 
          vuelve a cambiarse a la velocidad normal*/
        if (isSprinting)
        {
            sprintSpeed = sprintingSpeedMultiplier;
        }
        else
        {
            sprintSpeed = 1;
        }
    }
}
