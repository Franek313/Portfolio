using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Marker : MonoBehaviour, IPointerClickHandler, IPointerUpHandler
{
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private Effect effect = null;
    [SerializeField] private AM_Test ambience_manager = null;
    [SerializeField] private string soundPath = null, option = null;
    [SerializeField] private Vector2 stereo_position = Vector2.zero;
    [SerializeField] private RectTransform rt = null;
    [SerializeField] private GameObject stereoManager = null;

    [System.Obsolete]
    IEnumerator waiter(float time)
    {
        yield return new WaitForSeconds(time);
        Play();
    }

    [System.Obsolete] //przestarzałe ale działa ;)
    private IEnumerator LoadAudio(string audioName, string path)
    {
        WWW request = GetAudioFromFile(ambience_manager.ambience_directory_path +option+"/"+path+"/", audioName);
        print(ambience_manager.ambience_directory_path +path+"/"+audioName);
        yield return request;
        audioSource.clip = request.GetAudioClip();
        audioSource.clip.name = audioName;
        print(audioSource.clip.name);
        audioSource.Play();
        StartCoroutine(waiter(audioSource.clip.length + Random.Range(ambience_manager.min_delay, ambience_manager.max_delay)));
    }

    [System.Obsolete]
    private WWW GetAudioFromFile(string path, string filename)
    {
        string audioToLoad = string.Format(path + "{0}", filename);
        WWW request = new WWW(audioToLoad);
        return request;
    }

    [System.Obsolete]
    void Start()
    {
        //Przyporzadkowanie instancji do parenta, okreslenie jego komponentów
        stereoManager = GameObject.Find("StereoManager");
        transform.SetParent(stereoManager.transform);
        rt = stereoManager.GetComponent<RectTransform>();
        audioSource = gameObject.AddComponent<AudioSource>();
        
        GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        
        //Określenie pozycji spawnu markera
        Vector3 circle_distance = Random.insideUnitSphere *rt.sizeDelta.x/8;
        if (circle_distance.x <= 0) circle_distance.x -= rt.sizeDelta.x / 8;
        else circle_distance.x += rt.sizeDelta.x/8;
        if (circle_distance.y <= 0) circle_distance.y -= rt.sizeDelta.y / 8;
        else circle_distance.y += rt.sizeDelta.y / 8;
        circle_distance.z = 0;
        Vector3 final_position = stereoManager.transform.position + circle_distance;
        transform.position = final_position;

        //Ustawienia nazwy, tekstu, koloru markera
        Color color = Random.ColorHSV(0, 1, 0, 1, 1, 1);
        Color opposite_color = new Color(Color.white.r - color.r, Color.white.g - color.g, Color.white.b - color.b);
        Image image = GetComponentInChildren<Image>();
        TMP_Text nameText = GetComponentInChildren<TMP_Text>();
        image.color = color;
        nameText.text = name;
        nameText.color = opposite_color;

        //Ustawienia dźwięku stereo
        stereo_position.Set((transform.position.x - rt.position.x) / (rt.sizeDelta.x / 200), (transform.position.y - rt.position.y) / (rt.sizeDelta.y / 200));
        audioSource.panStereo = stereo_position.x / 100;
        audioSource.panStereo = stereo_position.x / 100;
        audioSource.volume = 1 - Mathf.Sqrt(Mathf.Pow(stereo_position.x, 2) + Mathf.Pow(stereo_position.y, 2)) / 100; 

        Play();
    }

    [System.Obsolete]
    public void Play()
    {
        DirectoryInfo info = new DirectoryInfo(Application.streamingAssetsPath + "/AMBIENCE/" + option + "/" + name);
        FileInfo[] files = info.GetFiles("*.mp3");

        if(files.Length != 0)
        {
            int i = Random.Range(0, files.Length);

            string[] s = files[i].ToString().Split('\\');
            s[0] = s[s.Length - 1].Split('.')[0];
            print(s[0]);
            
            StartCoroutine(LoadAudio(s[s.Length - 1], name));  
        }
    }

    public void SetEffect(Effect new_effect)
    {
        effect = new_effect;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            
            StartCoroutine(effect.DestroyMarker(gameObject, 1, 0));
        }
    }
    private void OnMouseDrag()
    {
        Vector2 anchoredPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, Input.mousePosition, Camera.main, out anchoredPos);
        GetComponent<RectTransform>().anchoredPosition = anchoredPos;
        stereo_position.Set((transform.position.x - rt.position.x) / (rt.sizeDelta.x / 200), (transform.position.y - rt.position.y) / (rt.sizeDelta.y / 200));
        audioSource.panStereo = stereo_position.x / 100;
        audioSource.panStereo = stereo_position.x / 100;
        audioSource.volume = 1 - Mathf.Sqrt(Mathf.Pow(stereo_position.x, 2) + Mathf.Pow(stereo_position.y, 2)) / 100;
    }

    private void OnMouseOver()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            StartCoroutine(effect.DestroyMarker(gameObject, 1, 0));
        }

        
    }

    public void SetOption(string new_option)
    {
        option = new_option;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(Vector3.Distance(transform.position, stereoManager.transform.position) > rt.sizeDelta.x/2)
        {
            StartCoroutine(effect.DestroyMarker(gameObject, 1, 0));
        }
    }
}
