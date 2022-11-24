using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Option : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public SpawnManager spawnManager;
    private TMP_Text text; //komponent TMP_Text 
    public int blockAdress; //numer bloku do którego prowadzi
    public string optionText; //tekst opcji
    
    public Option(int adress, string optionText) //podczas tworzenia obiektu opcja ustawiamy adres i tekst
    {
        this.optionText = optionText;
        this.blockAdress = adress;
    }

    void Start()
    {
       spawnManager = GameObject.FindObjectOfType<SpawnManager>();
       text = GetComponent<TMP_Text>(); //dodanie obiektu TMP_Text opcji do zmiennej text
       text.color = spawnManager.dialogueManager.normalColor; //ustawienie koloru opcji na normalny
    }

    public void OnPointerEnter(PointerEventData eventData) //najechanie = zmień kolor na hover
    {
        text.color = spawnManager.dialogueManager.hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData) //zjechanie = zmień kolor na normalny
    {
        text.color = spawnManager.dialogueManager.normalColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        spawnManager.DestroyCharacter();
        spawnManager.SpawnCharacter(blockAdress, spawnManager.currentCharacterName);
        if(blockAdress == 0)
        {
            spawnManager.dialogueManager.ExitDialogue();
        }
    }
}
    
