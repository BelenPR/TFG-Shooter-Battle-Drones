using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    //Esta variable almacena el componente "NavMeshAgent" del enemigo que tiene asignado este script
    public NavMeshAgent navMeshAgent;

    //Este array almacenar� los puntos entre los cuales se mover� nuestro enemigo
    public Transform[] destinations;

    //Variable que ir� variando para indicar los difernetes puntos dentro del array a los que el enemigo se desplazar�
    private int i = 0;

    //Variable que almacenar� la distancia a partir de la cual el enemigo empezar� a seguir otro de los puntos de los almacenados en el array
    public float distanceToFollowPath = 2f;

    [Header("----------FollowPlayer?----------")]

    //Este boolean controlar� si el enemigo est� o no siguiendo al jugador
    public bool followPlayer;

    //Variable que almacenar� al player
    private GameObject player;

    //Esta variable almacena la distancia entre el enemigo y el player
    private float distanceToPlayer;

    //Variable que almacenar� la distancia a partir de la cual el enemigo empezar� a seguir al player
    public float distanceToFollowPlayer = 10f;

    //Variable que almacena la vida del enemigo
    public int lifes = 3;


    // Start is called before the first frame update
    void Start()
    {
        /*Con este condicional controlamos que si el array que almacena los puntos entre los que se mueve el enemigo est� vac�o (no hay puntos)
          el script que se encarga del movimiento del enemigo se desactiva, y sino, se mueve entre estos puntos o siguiendo al enemigo*/
        if(destinations == null || destinations.Length == 0)
        {
            transform.gameObject.GetComponent<AI>().enabled = false;
        }
        else
        {
            //Con esta l�nea le indicamos al enemigo d�nde debe dirigirse cuando se inicia el juego
            navMeshAgent.destination = destinations[i].transform.position;
        
            //Esta variable almacenar� al jugador, identificandolo por ser el objeto que posee el script llamado "PlayerMovement"
            player = FindObjectOfType<PlayerMovement>().gameObject;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Esta variable almacenar� la distancia a la que se encuentra el enemigo del jugador
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        //Este condicional comprueba si la distancia que hay desde el enemigo al jugador es menor o igual a la distancia a partir de la cual le tiene que seguir, y si es as�, le sigue y sino, sigue su ruta habitual
        if(distanceToPlayer <= distanceToFollowPlayer && followPlayer)
        {
            FollowPlayer();
        }
        else
        {
            EnemyPath();
        }
    }

    //Este m�todo ser� llamado en cada frame para que el enemigo siga una ruta determinada sobre la malla de navegaci�n
    public void EnemyPath()
    {
        //Esta l�nea hace que el enemigo se empiece a desplazar hasta la posici�n de uno de los puntos almacenados en el array
        navMeshAgent.destination = destinations[i].position;

        //Si la distancia al punto al que se dirige es menor que la distancia a la cual debe seguir otro punto...
        if(Vector3.Distance(transform.position,destinations[i].position) <= distanceToFollowPath)
        {
            //si el punto al que se acaba de dirigir no es el �ltimo almacenado en el array, el enemigo se dirigir� al siguiente punto almacenado en el array y sino, se dirigir� al primer punto del array
            if(destinations[i] != destinations[destinations.Length - 1])
            {
                i++;
            }
            else
            {
                i = 0;
            }
        }
    }

    //Este m�todo ser� llamado cuando el enemigo detecte al player, para comenzar a seguirlo
    public void FollowPlayer()
    {
        navMeshAgent.destination = player.transform.position;
    }

    //Este m�todo ser� llamado cuando la explosi�n de una granada colisione con el enemigo y le restar� 2 vidas
    public void GrenadeImpact()
    {
        LoseLife(2);
    }

    /*M�todo que controlar� la p�rdida de la cantidad de vidas pasadas por par�metro, por parte del enemigo al recibir el impacto de una bala
      o una granada, y si esas vidas llegan a 0 se destruye el enemigo*/
    public void LoseLife(int lifesToLose)
    {
        lifes = lifes - lifesToLose;

        if (lifes <= 0)
        {
            Destroy(gameObject);
        }
    }
}
