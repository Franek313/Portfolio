using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public static class FadeIn
{
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.SmoothStep(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    public static IEnumerator StartFadeListener(float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = AudioListener.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            AudioListener.volume = Mathf.SmoothStep(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}

public static class FadeOut
{

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.SmoothStep(start, targetVolume, currentTime / duration);
            yield return null;
        }
        audioSource.volume = 1;
        audioSource.Stop();
        yield break;
    }

    public static IEnumerator StartFadePause(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.SmoothStep(start, targetVolume, currentTime / duration);
            yield return null;
        }
        audioSource.volume = 1;
        audioSource.Pause();
        yield break;
    }

}

public class MusicManager : MonoBehaviour
{
    public string current_path;
    private AudioSource audioSource, previous;
    private AudioClip current;
    private AudioClip[] genre;
    private float current_volume = 1f, previous_time;
    public float fadeIn = 3, fadeOut = 2;
    private string music_directory_path = Application.streamingAssetsPath + "/MUZYKA/"; 

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        previous = gameObject.AddComponent<AudioSource>();
        previous.loop = false;
    }

    [System.Obsolete]
    public void Play(string path)
    {
        StopAllCoroutines();
        current_path = path;
        
        var info = new DirectoryInfo(music_directory_path + path);
        print(music_directory_path + path);
        var list_of_files = info.GetFiles("*.mp3");
        var i = Random.Range(0, list_of_files.Length);
        string[] s = list_of_files[i].ToString().Split('\\');
        print(s[s.Length-1]);

        StartCoroutine(LoadAudio(s[s.Length-1], path));
        UpdateVolume(GameObject.Find("Pasek").GetComponent<Slider>().value);
    }

    [System.Obsolete]
    private IEnumerator waiter(float time)
    {
        yield return new WaitForSeconds(time);
        Play(current_path);
    }

    [System.Obsolete]
    private IEnumerator LoadAudio(string audioName, string path)
    {
        WWW request = GetAudioFromFile(music_directory_path + "/"+path+"/", audioName);
        yield return request;

        previous.clip = current;
        
        current = request.GetAudioClip();
        current.name = audioName;

        GameObject.Find("Nazwa").GetComponent<TMP_Text>().text = current.name.Remove(current.name.Length - 4,4);

        
        previous.time = Mathf.Min(audioSource.time, current.length - 0.001f); //Mathf.Min sluzy tylko do wyeliminowania nieszkodliwego bledu w konsoli, normalnie mogloby byc previous.time = audioSource.time;
        previous.Play();

        audioSource.clip = current;
        audioSource.Play();
        audioSource.volume = 0;
        isPaused = false;

            StartCoroutine(FadeOut.StartFade(previous, fadeOut, 0));
            StartCoroutine(FadeIn.StartFade(audioSource, fadeIn, current_volume));
       
        StartCoroutine(waiter(current.length));

    }

    [System.Obsolete]
    private WWW GetAudioFromFile(string path, string filename)
    {
        string audioToLoad = string.Format(path + "{0}", filename);
        WWW request = new WWW(audioToLoad);
        return request;
    }

    public void Stop()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut.StartFade(audioSource, 1, 0));
        isPaused = true;
        
    }

    public void Mute()
    {
        if (AudioListener.volume == 0) StartCoroutine(FadeIn.StartFadeListener(1, 1));
        else StartCoroutine(FadeIn.StartFadeListener(1, 0));

    }

    bool isPaused = true;
    public void Pause()
    {
        StopAllCoroutines();
        if (isPaused == false)
        {
            StartCoroutine(FadeOut.StartFadePause(audioSource, 1, 0));
            isPaused = true;
        }
        else
        {
            audioSource.Play();
            audioSource.volume = 0;
            StartCoroutine(FadeIn.StartFade(audioSource, 1, current_volume));
            isPaused = false;
        }
    }

    public void UpdateVolume(float volume)
    {
        audioSource.volume = volume;
        current_volume = volume;
        previous.volume = volume;
        GameObject.Find("Level").GetComponentInChildren<TMP_Text>().text = Mathf.RoundToInt((volume * 100)).ToString() + "%";
    }

    public void ResetVolume()
    {
        GameObject.Find("Pasek").GetComponent<Slider>().value = 1f;
    }

    void Update()
    {
        current_volume = GameObject.Find("Pasek").GetComponent<Slider>().value;
    }


 
}


