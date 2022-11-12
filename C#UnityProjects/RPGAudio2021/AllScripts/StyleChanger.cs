using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class StyleSheet
{
    public string nazwa;
    [Header("Music Buttons")]
    public ColorBlock MusicButton = new ColorBlock();
    public Color MusicTextColor;
    [Header("Ambience Buttons")]
    public ColorBlock AmbienceButton = new ColorBlock();
    public Color AmbienceTextColor;
    [Header("Monster Buttons")]
    public ColorBlock MonsterButton = new ColorBlock();
    public Color MonsterTextColor;

}
public class StyleChanger : MonoBehaviour
{
    [Header("Color Blocks Settings")]
    public List<StyleSheet> UIStyle;
    public GameObject[] music_buttons, amb_buttons, monster_buttons;

    private void Awake()
    {
        
    }

    public void ChangeStyle(int nr)
    {
        music_buttons = GameObject.FindGameObjectsWithTag("MusicButton");
        amb_buttons = GameObject.FindGameObjectsWithTag("AmbienceButton");
        monster_buttons = GameObject.FindGameObjectsWithTag("MonsterButton");

        for(var i=0; i<music_buttons.Length; i++)
        {
            music_buttons[i].GetComponent<Button>().colors = UIStyle[nr].MusicButton;
            music_buttons[i].GetComponentInChildren<Text>().color = UIStyle[nr].MusicTextColor;
        }

        foreach (GameObject gO in amb_buttons)
        {
            gO.GetComponent<Button>().colors = UIStyle[nr].AmbienceButton;
            gO.GetComponentInChildren<Text>().color = UIStyle[nr].AmbienceTextColor;
        }

        foreach (GameObject gO in monster_buttons)
        {
            gO.GetComponent<Button>().colors = UIStyle[nr].MonsterButton;
            gO.GetComponentInChildren<Text>().color = UIStyle[nr].MonsterTextColor;
        }
    }
}
