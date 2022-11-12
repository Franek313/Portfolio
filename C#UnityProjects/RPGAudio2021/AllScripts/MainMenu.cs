using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject button;
    public List<GameObject> genres;
    private Vector3 hover = new Vector3(0.05f, 0.05f, 0);

    private void Start()
    {
        button = gameObject;
    }

    private void OnMouseEnter()
    {
        print("JEST!");
        button.GetComponent<Transform>().localScale += hover;
    }

    private void OnMouseExit()
    {
        button.GetComponent<Transform>().localScale -= hover;
    }

    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);

    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ChooseGenre(int nr)
    {
        foreach(GameObject g in genres)
        {
            g.SetActive(true);
        }

        foreach(GameObject g in genres)
        {
            if(g != genres[nr])
            {
                g.SetActive(false);
            }
        }
    }
}


