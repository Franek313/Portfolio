using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public GameObject characterSlot = null;
    public GameObject characterPrefab;
    public GameObject character;
    public string currentCharacterName;

    void Start()
    {
        dialogueManager.StartDialogue();
        SpawnCharacter(1, "Julia");
    }

    public void SpawnCharacter(int startBlockID, string characterName)
    {
        currentCharacterName = characterName;
        GameObject newCharacter = characterPrefab;
        newCharacter.GetComponent<Character>().startID = startBlockID;
        newCharacter.GetComponent<Character>().characterName = characterName;

        character = Instantiate(newCharacter, characterSlot.transform); //spawnuje postać
    }

    public void DestroyCharacter()
    {
        Destroy(character);
        dialogueManager.DestroyBlock();
    }
}
