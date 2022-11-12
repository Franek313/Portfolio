using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swaping : MonoBehaviour
{
    public GameObject muzyka, ambience;

    void Start()
    {
        muzyka.SetActive(true);
        ambience.SetActive(false);
    }

    public void Swap()
    {
        if(muzyka.activeSelf)
        {
            ambience.SetActive(true);
            muzyka.SetActive(false);
        }
        else
        {
            ambience.SetActive(false);
            muzyka.SetActive(true);
        }
    }
}

