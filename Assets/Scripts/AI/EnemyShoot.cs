using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    //Variable que almacena el objeto que será la bala del enemigo
    public GameObject enemyBullet;
    
    //Variable que almacena el punto de instanciación (de aparición) de las balas
    public Transform spawnBulletPoint;

    //Variable que almacena la posición del player
    private Transform playerPosition;

    //Variable que almacena la velocidad de la bala
    public float bulletVelocity = 100f;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = FindObjectOfType<PlayerMovement>().transform;

        //Método que se utiliza para llamar a otro método cada cierto tiempo
        Invoke("ShootPlayer", 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShootPlayer()
    {
        //Dirección hacia la que se disparará la bala, que será la posición del player
        Vector3 playerDirection= playerPosition.position - transform.position;

        //Nueva bala que se va a instanciar
        GameObject newBullet;
        
        //Línea que instancia la bala en un punto determinado
        newBullet = Instantiate(enemyBullet, spawnBulletPoint.position, spawnBulletPoint.rotation);

        //Línea que hace que la bala sea disparada en una direción y con una fuerza determinadas
        newBullet.GetComponent<Rigidbody>().AddForce(playerDirection * bulletVelocity, ForceMode.Force);

        //Método que se utiliza para llamar a otro método cada cierto tiempo
        Invoke("ShootPlayer", 3);
    }
}
