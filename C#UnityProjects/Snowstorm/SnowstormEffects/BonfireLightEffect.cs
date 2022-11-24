//Simple effect that changes campfire (bonfire) colors during the time making impression of fire burning light model

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireLightEffect : MonoBehaviour
{
    [SerializeField]
    Light light;
    Vector3 position;
    
    [Tooltip("The lower Step value, the worse performance of the game")]
    public float light_distortion = 0.05f,
                 steps = 0.01f;

    public List<Color> colors;

    public IEnumerator WaitAndChangeLerpValue()
    {
        transform.position = Vector3.Lerp(transform.position, position, 0.1f);
        yield return new WaitForSeconds(steps);

        light.color = Color.Lerp(light.color, colors[Random.Range(0,colors.Count)], light_distortion);

        light.intensity = Mathf.Lerp(light.intensity, Random.Range(2,4), light_distortion);
             
        StartCoroutine(WaitAndChangeLerpValue());
    }

    void Start()
    {
        light = GetComponent<Light>();
        position = transform.position;
        StartCoroutine(WaitAndChangeLerpValue());
    }

}
