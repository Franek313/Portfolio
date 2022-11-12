//Manager script that controlls spawning of effect (usually particles) in the game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EffectManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> effects = new List<GameObject>();

    public GameObject InstantiateEffect(int index, Transform t)
    {
        GameObject effect = Instantiate(effects[index], t);
        effect.transform.SetParent(null);
        return effect;
    }

    public GameObject InstantiateEffect(int index, Vector3 position, Quaternion rotation)
    {
        GameObject effect = Instantiate(effects[index], position, rotation);
        effect.transform.SetParent(null);
        return effect;
    }
  
}
