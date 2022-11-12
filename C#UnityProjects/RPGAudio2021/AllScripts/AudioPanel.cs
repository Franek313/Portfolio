using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AudioPanel : MonoBehaviour
{
    public RectTransform m_parent, m_image;
    public Camera ui_Cam;
    private AudioSource audioSource;
    private Vector2 stereo_position;
    private CircleCollider2D circleCollider2d;
    public GameObject myPrefab, myParent;
    public int index;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        circleCollider2d = GetComponent<CircleCollider2D>();
        circleCollider2d.radius = m_parent.sizeDelta.x / 2;
        Debug.Log(m_image.transform.position);
        stereo_position.Set(m_image.transform.position.x - 213, m_image.transform.position.y - 568);
        
    }
    void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 anchoredPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_parent, Input.mousePosition, ui_Cam, out anchoredPos);
            m_image.anchoredPosition = anchoredPos;
            Debug.Log(m_image.transform.position);

            stereo_position.Set((m_image.transform.position.x - m_parent.position.x)/(m_parent.sizeDelta.x/200) , (m_image.transform.position.y - m_parent.position.y)/(m_parent.sizeDelta.y / 200));
            audioSource.panStereo = stereo_position.x/100;
            audioSource.volume = 1 - Mathf.Sqrt(Mathf.Pow(stereo_position.x, 2) + Mathf.Pow(stereo_position.y, 2))/100;
            Debug.Log(stereo_position);
        }
    }
    public void AddMarker(int index)
    {
    
        Instantiate(myPrefab, myParent.transform);
    }
}
