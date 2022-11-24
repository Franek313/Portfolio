using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Color normalColor, 
                 hoverColor, 
                 characterColor;
    public GameObject dialoguePanel;
    public TMP_Text currentSpeaker = null; //przechowuje obiekt imienia aktualnie mówiącej postaci ze sceny
    public TMP_Text characterText = null; //przechowuje tekst postaci ze sceny
    public GameObject option = null; //Prefab opcji
    public GameObject optionsPanel = null; //Panel opcji
    public string playerName = "NoName"; //imie gracza

    public void SetCurrentSpeaker(string newSpeaker) //ustaw imie mówiącej aktualnie postaci
    {
        currentSpeaker.text = newSpeaker;
        currentSpeaker.color = normalColor;
    }

    public void SetCharacterText(string newCharacterText) //ustaw tekst aktualnie mówiącej postaci
    {
        characterText.text = newCharacterText;
        characterText.color = hoverColor;
    }

    public void AddOption(int adress, string optionText) //dodaj opcje i ustaw prefab
    {
        GameObject newOption = Instantiate(option, optionsPanel.transform);
        newOption.GetComponent<Option>().optionText = optionText;
        newOption.GetComponent<TMP_Text>().text = optionText;
        newOption.GetComponent<Option>().blockAdress = adress;
    }

    public void LoadBlock(DialogueBlock dialogueBlock) //ładuje blok dialogowy
    {
        SetCharacterText(dialogueBlock.characterText);

        foreach(Option o in dialogueBlock.options)
        {
            AddOption(o.blockAdress, o.optionText);
        }
    }

    public void DestroyBlock() //usuwa blok dialogowy
    {
        foreach(Transform t in optionsPanel.transform)
        {
            characterText.text = "";
            Destroy(t.gameObject);
        }
    }

    public void StartDialogue()
    {
        dialoguePanel.SetActive(true);
    }

    public void ExitDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    public void SetDialoguePanelColor()
    {
        Color temp = characterColor;
        temp.a = 0.8f;
        dialoguePanel.GetComponent<Image>().color = temp;
    }



}
