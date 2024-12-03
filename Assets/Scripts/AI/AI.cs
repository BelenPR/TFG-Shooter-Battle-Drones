using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    //Esta variable almacena el componente "NavMeshAgent" del enemigo que tiene asignado este script
    public NavMeshAgent navMeshAgent;

    //Este array almacenará los puntos entre los cuales se moverá nuestro enemigo
    public Transform[] destinations;

    //Variable que irá variando para indicar los difernetes puntos dentro del array a los que el enemigo se desplazará
    private int i = 0;

    //Variable que almacenará la distancia a partir de la cual el enemigo empezará a seguir otro de los puntos de los almacenados en el array
    public float distanceToFollowPath = 2f;

    [Header("----------FollowPlayer?----------")]

    //Este boolean controlará si el enemigo está o no siguiendo al jugador
    public bool followPlayer;

    //Variable que almacenará al player
    private GameObject player;

    //Esta variable almacena la distancia entre el enemigo y el player
    private float distanceToPlayer;

    //Variable que almacenará la distancia a partir de la cual el enemigo empezará a seguir al player
    public float distanceToFollowPlayer = 10f;

    //Variable que almacena la vida del enemigo
    public int lifes = 3;


    // Start is called before the first frame update
    void Start()
    {
        /*Con este condicional controlamos que si el array que almacena los puntos entre los que se mueve el enemigo está vacío (no hay puntos)
          el script que se encarga del movimiento del enemigo se desactiva, y sino, se mueve entre estos puntos o siguiendo al enemigo*/
        if(destinations == null || destinations.Length == 0)
        {
            transform.gameObject.GetComponent<AI>().enabled = false;
        }
        else
        {
            //Con esta línea le indicamos al enemigo dónde debe dirigirse cuando se inicia el juego
            navMeshAgent.destination = destinations[i].transform.position;
        
            //Esta variable almacenará al jugador, identificandolo por ser el objeto que posee el script llamado "PlayerMovement"
            player = FindObjectOfType<PlayerMovement>().gameObject;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Esta variable almacenará la distancia a la que se encuentra el enemigo del jugador
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        //Este condicional comprueba si la distancia que hay desde el enemigo al jugador es menor o igual a la distancia a partir de la cual le tiene que seguir, y si es así, le sigue y sino, sigue su ruta habitual
        if(distanceToPlayer <= distanceToFollowPlayer && followPlayer)
        {
            FollowPlayer();
        }
        else
        {
            EnemyPath();
        }
    }

    //Este método será llamado en cada frame para que el enemigo siga una ruta determinada sobre la malla de navegación
    public void EnemyPath()
    {
        //Esta línea hace que el enemigo se empiece a desplazar hasta la posición de uno de los puntos almacenados en el array
        navMeshAgent.destination = destinations[i].position;

        //Si la distancia al punto al que se dirige es menor que la distancia a la cual debe seguir otro punto...
        if(Vector3.Distance(transform.position,destinations[i].position) <= distanceToFollowPath)
        {
            //si el punto al que se acaba de dirigir no es el último almacenado en el array, el enemigo se dirigirá al siguiente punto almacenado en el array y sino, se dirigirá al primer punto del array
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

    //Este método será llamado cuando el enemigo detecte al player, para comenzar a seguirlo
    public void FollowPlayer()
    {
        navMeshAgent.destination = player.transform.position;
    }

    //Este método será llamado cuando la explosión de una granada colisione con el enemigo y le restará 2 vidas
    public void GrenadeImpact()
    {
        LoseLife(2);
    }

    /*Método que controlará la pérdida de la cantidad de vidas pasadas por parámetro, por parte del enemigo al recibir el impacto de una bala
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
