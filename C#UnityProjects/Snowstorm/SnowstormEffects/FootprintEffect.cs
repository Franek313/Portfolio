//This effect spawns particles (footsteps) coworking with EffectManager class

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootprintEffect : MonoBehaviour
{
    [SerializeField] private EffectManager effectManager = null;
    [SerializeField] private Transform foot_left = null, foot_right = null, player = null;

    void Start()
    {
        player = gameObject.transform;

        if(effectManager == null)
        effectManager = GameObject.FindObjectOfType<EffectManager>();
    }
  
    public void SpawnFootprintLeft() //animation event activates this methode on left foot step
    {
        GameObject footprint = effectManager.InstantiateEffect(0, foot_left);
    }

    public void SpawnFootprintRight()
    {
        GameObject footprint = effectManager.InstantiateEffect(0, foot_right);
    }

    
}
