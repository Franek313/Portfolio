using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class AM_Test : MonoBehaviour
{
    public int min_delay = 0, max_delay = 5; 
    public string current_option = null;   
    public string ambience_directory_path = Application.streamingAssetsPath + "/AMBIENCE/"; 
    [SerializeField] private GameObject panel = null, effect = null;
    [SerializeField] private TMP_Dropdown effect_types = null;
    public List<string> files = new List<string>();

    void Start()
    {
        List<string> dropDownOptions = new List<string>();
        effect_types.ClearOptions();
        
        DirectoryInfo info = new DirectoryInfo(ambience_directory_path);
        FileInfo[] fileInfo = info.GetFiles();

        foreach(FileInfo file in fileInfo) 
        {
            string[] f = file.ToString().Split('\\');
            dropDownOptions.Add(f[f.Length-1].Split('.')[0]);
        }
        effect_types.AddOptions(dropDownOptions);

        ChangeOptionList();
        
    }

    public void InstantiateEffects(GameObject effect, List<string> files)
    {
        foreach(Transform t in panel.transform)
        {
            Destroy(t.gameObject);
        }

        foreach(string f in files)
        {
            GameObject instance = Instantiate(effect, panel.transform);
            instance.name = f;
            instance.GetComponent<Effect>().SetOption(current_option);
        }
    }

    public void ChangeOptionList()
    {
        current_option = effect_types.options[effect_types.value].text;
        string pathToOption = ambience_directory_path + current_option;
        DirectoryInfo info = new DirectoryInfo(pathToOption);
        FileInfo[] fileInfo = info.GetFiles("*.meta");

        files.Clear();
        foreach(FileInfo file in fileInfo) 
        {
            string[] f = file.ToString().Split('\\');
            string[] splited = f[f.Length-1].Split('.');
            files.Add(f[f.Length-1].Split('.')[0]);
        }
        InstantiateEffects(effect, files);
    }
}
