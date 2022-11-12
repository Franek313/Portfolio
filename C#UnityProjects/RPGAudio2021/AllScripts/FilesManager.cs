using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FilesManager : MonoBehaviour
{
    [SerializeField] private string music_data_path = Application.dataPath + "/MusicData.txt",
                                    ambience_data_path = Application.dataPath + "/AmbienceData.txt";
    void Awake()
    {
        StreamWriter m_file = File.CreateText(music_data_path);
        StreamWriter a_file = File.CreateText(ambience_data_path);

        DirectoryInfo[] a_dir_info = new DirectoryInfo(Application.streamingAssetsPath + "/AMBIENCE/").GetDirectories();
        
        foreach(DirectoryInfo dI in a_dir_info)
        {
            a_file.WriteLine(dI.ToString());
            DirectoryInfo[] a_subdir1_info = new DirectoryInfo(dI.ToString()).GetDirectories();
            foreach(DirectoryInfo sd1I in a_subdir1_info)
            {
                a_file.WriteLine("\t" + sd1I.ToString());
            }
        }
        a_file.Close(); 
    }
}
