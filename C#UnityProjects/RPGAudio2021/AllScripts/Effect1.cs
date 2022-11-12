using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MusicGenre : Effect, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private bool is_clicked = false;
    [SerializeField] private GameObject marker = null;
    [SerializeField] private List<GameObject> my_markers = new List<GameObject>();
    [SerializeField] private string option = null;
    public Color normal_color = Color.white, 
                 hover_color = Color.white, 
                 clicked_color = Color.white, 
                 disabled_color = Color.white;

    public IEnumerator DestroyMarker(GameObject marker, float duration, float targetVolume)
    {
        foreach(Transform t in marker.transform)
        {
            t.gameObject.SetActive(false);
        }
        
        AudioSource audioSource = marker.GetComponent<AudioSource>();
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.SmoothStep(start, targetVolume, currentTime / duration);
            yield return null;
        }
        Destroy(marker);
        yield break;
    }

    public void Start()
    {
        image.color = normal_color;
        nameText.text = name;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            AddMarker();
            image.color = clicked_color;
            is_clicked = true;
        }
        
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            foreach(GameObject gO in my_markers)
            {
                StartCoroutine(DestroyMarker(gO, 1,0));
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!is_clicked)
        image.color = hover_color;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!is_clicked)
        image.color = normal_color;
    }

    public void AddMarker()
    {
        GameObject new_marker = Instantiate(marker);
        new_marker.name = name;
        new_marker.GetComponent<Marker>().SetEffect(this);
        new_marker.GetComponent<Marker>().SetOption(option);
        my_markers.Add(new_marker);
    }

    public void SetOption(string new_option)
    {
        option = new_option;
    }

}
