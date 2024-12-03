using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Propiedad de la clase "GameManager" que hace referencia al texto del canva que nos dice la munici�n que tenemos
    public Text ammoText;

    //Estas l�neas son un patr�n de dise�o Singleton, que asegura que solo haya una �nica instancia de la clase "GameManager" en todo el juego,
    //pero se podr� acceder al valor de sus propiedades desde cualquier script, aunque dichos valores solo puedan modificarse dentro de esta clase.
    public static GameManager Instance { get; private set; }

    //Propiedad de la clase "GameManager" que inicialmente otorga 10 balas de munici�n al arma
    public int gameAmmo = 10;

    //Propiedad de la clase "GameManager" que hace referencia al texto del canva que nos dice la cantidad de vida que tenemos
    public Text healthText;

    //Propiedad que almacena la vida inicial
    public int health = 100;

    //Propiedad que almacena la vida m�xima
    public int maxHealth = 100;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        ammoText.text = gameAmmo.ToString();
        healthText.text = health.ToString();
    }

    public void LoseHealth(int healthToReduce)
    {
        health -= healthToReduce;
        CheckHealth();
    }

    public void CheckHealth()
    {
        if(health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void AddHealth(int health)
    {
        if(this.health + health >= maxHealth)
        {
            this.health = 100;
        }
        else
        {
            this.health += health; 
        }
    }
}
