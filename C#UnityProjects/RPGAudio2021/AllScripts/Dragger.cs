using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dragger : MonoBehaviour
{
    public RectTransform m_parent, m_image;
    private Camera ui_Cam;
    private Vector2 stereo_position;
    private Image img;

    // Start is called before the first frame update
    void Start()
    {
        img = gameObject.GetComponent<Image>();
        img.color = Random.ColorHSV(0,1,0.5f,1,1,1);
        m_image = gameObject.GetComponent<RectTransform>();
        m_parent = GameObject.Find("StereoManager").GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Sqrt(Mathf.Pow(stereo_position.x, 2) + Mathf.Pow(stereo_position.y, 2)) > 100 && Input.GetMouseButtonUp(0))
        {
            Destroy(gameObject);
        }

    }

    private void OnMouseDrag()
    {
            Vector2 anchoredPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_parent, Input.mousePosition, ui_Cam, out anchoredPos);
            m_image.anchoredPosition = anchoredPos;
            stereo_position.Set((m_image.transform.position.x - m_parent.position.x) / (m_parent.sizeDelta.x / 200), (m_image.transform.position.y - m_parent.position.y) / (m_parent.sizeDelta.y / 200));
            Debug.Log(m_image.transform.position);
            Debug.Log(stereo_position);
    }

}
