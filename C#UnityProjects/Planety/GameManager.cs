using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //ORE SETTINGS
    public static int minOreCount = 1;
    public static int maxOreCount = 5;
    public static int oreMinSize = 5;
    public static int oreMaxSize = 15;
    public static Color fuelColor = new Color(0.349f, 0.859f, 0.384f);
    public static Color metalColor = new Color(1f, 0.647f, 0f);
    public static Color metalBoxColor = new Color(0.8f, 0.8f, 0.8f);
    public static Color waterColor = new Color(0.267f, 0.608f, 0.871f);

    //BOX
    public static int boxQuantity = 2;

    //FUEL
    public static int maxFuel = 10;

    //INPUT
    public static KeyCode moveUp = KeyCode.W;
    public static KeyCode moveDown = KeyCode.S;
    public static KeyCode moveLeft = KeyCode.A;
    public static KeyCode moveRight = KeyCode.D;
    public static KeyCode jump = KeyCode.Space;
    public static KeyCode run = KeyCode.LeftShift;
    public static KeyCode switchLeft = KeyCode.Q;
    public static KeyCode switchRight = KeyCode.E;
    public static KeyCode pickUp = KeyCode.Mouse0;
    public static KeyCode useItem = KeyCode.Mouse1;
    public static KeyCode exit = KeyCode.Escape;

    //SCENE
    public static GameObject player;
    public static CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        virtualCamera = GameObject.FindGameObjectWithTag("CMMainCamera").GetComponent<CinemachineVirtualCamera>() ;
    }

    public GameObject exitPanel;
    bool gamePaused = false;
    public void Update()
    {
        if(Input.GetKeyDown(exit))
        {
            if(!gamePaused)
            {
                PauseGame(exitPanel);
            }
            else
            {
                UnpauseGame(exitPanel);
            } 
        }
    }

    public void LoadScene(int index)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame(GameObject panel)
    {
        Time.timeScale = 0;
        panel.SetActive(true);
        gamePaused = true;
    }

    public void UnpauseGame(GameObject panel)
    {
        Time.timeScale = 1;
        panel.SetActive(false);
        gamePaused = false;
    }

}
