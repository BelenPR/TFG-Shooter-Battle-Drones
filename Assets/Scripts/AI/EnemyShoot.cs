using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    //Variable que almacena el objeto que ser� la bala del enemigo
    public GameObject enemyBullet;
    
    //Variable que almacena el punto de instanciaci�n (de aparici�n) de las balas
    public Transform spawnBulletPoint;

    //Variable que almacena la posici�n del player
    private Transform playerPosition;

    //Variable que almacena la velocidad de la bala
    public float bulletVelocity = 100f;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = FindObjectOfType<PlayerMovement>().transform;

        //M�todo que se utiliza para llamar a otro m�todo cada cierto tiempo
        Invoke("ShootPlayer", 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShootPlayer()
    {
        //Direcci�n hacia la que se disparar� la bala, que ser� la posici�n del player
        Vector3 playerDirection= playerPosition.position - transform.position;

        //Nueva bala que se va a instanciar
        GameObject newBullet;
        
        //L�nea que instancia la bala en un punto determinado
        newBullet = Instantiate(enemyBullet, spawnBulletPoint.position, spawnBulletPoint.rotation);

        //L�nea que hace que la bala sea disparada en una direci�n y con una fuerza determinadas
        newBullet.GetComponent<Rigidbody>().AddForce(playerDirection * bulletVelocity, ForceMode.Force);

        //M�todo que se utiliza para llamar a otro m�todo cada cierto tiempo
        Invoke("ShootPlayer", 3);
    }
}
