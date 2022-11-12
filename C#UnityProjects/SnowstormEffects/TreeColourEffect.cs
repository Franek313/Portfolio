//Simple script changing colors of the trees (or any other gO with compatible shader materials) to the ones from List
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeColourEffect : MonoBehaviour
{
    public List<Color> treeColors = new List<Color>();
    void Start()
    {
        Material mat = GetComponentInChildren<Renderer>().material;
        mat.color = treeColors[Random.Range(0,treeColors.Count)];
    }

}
