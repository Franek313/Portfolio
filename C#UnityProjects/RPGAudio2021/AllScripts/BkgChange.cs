using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class BkgChange : MonoBehaviour
{
    public Sprite last_cliked_bkg = null;
    public Sprite current_bkg = null;
    public void ChangeBkg(string path)
    {
        GameObject.Find("Kanwa").GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
    }

    public void SetBkg(string path)
    {
        last_cliked_bkg = Resources.Load<Sprite>(path);
    }

    public void GetLastClicked()
    {
        GameObject.Find("Kanwa").GetComponent<Image>().sprite = last_cliked_bkg;
    }
}
