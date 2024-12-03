using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    //Variable que almacena el panel de pausa
    public GameObject pausePanel;

    //Variable que controlar� si el juego est� en pausa
    private bool isGamePaused = false;

    // Update is called once per frame
    void Update()
    {
        //Condicional que controla que cuando pulsamos la tecla "P" se pausa el juego si �ste no estaba pausado, o se despausa si este estaba pausado.
        if (Input.GetKeyDown(KeyCode.P))
        {
            isGamePaused = !isGamePaused;
            PauseGame();
        }
    }

    //M�todo que se encargar� de pausar o despausar el juego y activar o desactivar el panel de pausa
    public void PauseGame()
    {
        if (isGamePaused)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
    }

}
