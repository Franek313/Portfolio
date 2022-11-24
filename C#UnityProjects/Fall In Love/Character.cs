using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public struct DialogueBlock //blok dialogowy
{
    public int ID; //Id bloku, używane do znalezienia bloku w tekście na podstawie "#numer"
    public string characterText; //Tekst postaci z którą gadamy
    public List<Option> options; //Lista opcji 
    public void Clear()
    {
        ID = 0;
        characterText = "";
        options.Clear();
    }
}



public class Character : MonoBehaviour
{
    public Material characterMaterial;
    public string[] characterStates = {"Idle","Wink","Shock","Love","Sad"};
    private DialogueManager dialogueManager; //do ustawiania dialogów na scenie
    public string characterName = "unnamed"; //imie postaci
    public int adress = 0;
    public int startID = 1;
    
    void Start()
    {

        dialogueManager = GameObject.FindObjectOfType<DialogueManager>(); //znajdujemy dialogueManagera

        ChangeCharacterState(0);

        StreamReader sr = new StreamReader(Application.streamingAssetsPath
                                           +"/Characters/"+characterName
                                           +"/"+characterName+".txt"); //odczyt pliku tekstowego postaci

        string line = sr.ReadLine(); //czytamy pierwszą linię (imię postaci)
        characterName = line; //przypisujemy imię postaci do characterName

        dialogueManager.characterColor = ReadColor(sr.ReadLine());
        dialogueManager.normalColor = ReadColor(sr.ReadLine());
        dialogueManager.hoverColor = ReadColor(sr.ReadLine());

        dialogueManager.SetCurrentSpeaker(characterName); //dialogueManager ustawia na scenie imię postaci

        characterMaterial = new Material(GetComponent<Image>().material);
        characterMaterial.SetColor("_SolidOutline", dialogueManager.characterColor);
        characterMaterial.SetFloat("_Thickness", 7);
        GetComponent<Image>().material = characterMaterial;

        dialogueManager.SetDialoguePanelColor();

        DialogueBlock dialogueBlock = new DialogueBlock();
        dialogueBlock.options = new List<Option>();
        
        do
        {
            line = sr.ReadLine();
        }while(!line.StartsWith("#"+startID));
        
        while(line != null)
        {   
            
            if(line == "#")
            {
                print("EndOfBlock: " + dialogueBlock.ID);
                dialogueManager.LoadBlock(dialogueBlock);
                dialogueBlock.Clear();
                break;
            } 
            else if(line.StartsWith("#"+startID))
            {
                int getID = int.Parse(line.Substring(line.LastIndexOf('#') + 1));
                dialogueBlock.ID = getID;
                print("StartOfBlock: "+dialogueBlock.ID);
            } 
            else if(line.StartsWith('*'))
            {
                if(line.Contains("{name}"))
                {
                    line = line.Replace("{name}", dialogueManager.playerName);
                }

                if(line.Contains("{characterName}"))
                {
                    line = line.Replace("{characterName}", characterName);
                }

                if(line.Contains(":)"))
                {
                    ChangeCharacterState(0);
                    line = line.Remove(line.LastIndexOf(":)"));
                }

                if(line.Contains(";)"))
                {
                    ChangeCharacterState(1);
                    line = line.Remove(line.LastIndexOf(";)"));
                }

                if(line.Contains(":O"))
                {
                    ChangeCharacterState(2);
                    line = line.Remove(line.LastIndexOf(":O"));
                }

                if(line.Contains(":o"))
                {
                    ChangeCharacterState(2);
                    line = line.Remove(line.LastIndexOf(":o"));
                }

                if(line.Contains("<3"))
                {
                    ChangeCharacterState(3);
                    line = line.Remove(line.LastIndexOf("<3"));
                }

                if(line.Contains(":("))
                {
                    ChangeCharacterState(4);
                    line = line.Remove(line.LastIndexOf(":("));
                }

                dialogueBlock.characterText = line.Remove(0,1);
                print(dialogueBlock.characterText);
            }
            else if(line.StartsWith('0') || line.StartsWith('1') || line.StartsWith('2') || line.StartsWith('3'))
            {
                if(line.Contains("{name}"))
                {
                    line = line.Replace("{name}", dialogueManager.playerName);
                }

                if(line.Contains("{characterName}"))
                {
                    line = line.Replace("{characterName}", characterName);
                }

                if(line.Contains("&"))
                {
                    adress = int.Parse(line.Substring(line.LastIndexOf('&') + 1));
                    line = line.Remove(line.LastIndexOf('&'));
                }

                string optionText = "- " + line.Remove(0,1);

                if(!line.StartsWith('0'))
                {
                    Option o = new Option(adress, optionText);

                    dialogueBlock.options.Add(o);

                    print(adress +" "+optionText);
                }
                else
                {
                    Option o = new Option(0, optionText);

                    dialogueBlock.options.Add(o);

                    print("Exit option: "+optionText);
                }
                
            }
            line = sr.ReadLine();
            print(startID);
        } 
        sr.Close();
    }

    public Sprite LoadImageFromPath(string imageNameWithExtension)
    {
        string imagePath = Application.streamingAssetsPath
                                           +"/Characters/"+characterName
                                           +"/"+imageNameWithExtension;
        byte[] pngBytes = System.IO.File.ReadAllBytes(imagePath);

        Texture2D tex2D = new Texture2D(1024,1024);
        tex2D.LoadImage(pngBytes);
        Sprite fromTex = Sprite.Create(tex2D, new Rect(0.0f, 0.0f, tex2D.width, tex2D.height),
                                       new Vector2(0.5f, 0.5f), 100f);

        print(imagePath);
        return fromTex;
    }

    public void ChangeCharacterState(int stateIndex)
    {
        GetComponent<Image>().sprite = LoadImageFromPath("SP_"+characterName+"_"
                                                        +characterStates[stateIndex]
                                                        +".png"); //ładowanie obrazu postaci ze StrAss
    }

    public Color ReadColor(string colorsString)
    {
        string[] colors = colorsString.Split(' ');
        print("R: "+int.Parse(colors[0])+" G: "+ int.Parse(colors[1])+" B: "+int.Parse(colors[2]));
        return new Color((float)int.Parse(colors[0])/255, (float)int.Parse(colors[1])/255, (float)int.Parse(colors[2])/255);
    }
}
