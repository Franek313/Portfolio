using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Mine : FuelableObject
{
    [Header("Mining")]
    public bool isWorking = false;
    public LineRenderer signal;
    public Transform signalStart, signalEnd;

    public Ore ore;
    public int oreIndex = 0;

    public GameObject boxPrefab;
    public Transform boxSpawner;
    public int efficiency = 5;

    public int substractSpeed = 2;

    public Animator animator = null;

    public Button switchButton;

    void Start()
    {
        if(ore)
        signalEnd = ore.signalEnd;

        signal.SetPosition(0, signalStart.localPosition);

        fuelBar.localScale = new Vector3(fuelQuantity * 0.1f, 1, 1);

        SetPlanet();
    }

    void Update()
    {
        Interact();
    }
    
    public IEnumerator SubstractFuel()
    {
        yield return new WaitForSeconds(substractSpeed);
        if(fuelQuantity > 0 && ore.size > 0)
        {
            StartCoroutine(SubstractFuel());
            print("SPAWN!");
            UseFuel(1);
            SpawnBox();
        }
        else
        {
            animator.SetBool("Working", false);
            StopAllCoroutines();
        }
    }

    void FixedUpdate()
    {
        if (!ore || ore.size <= 0)
        {
            animator.SetBool("Working", false);
            StopAllCoroutines();
        }
    }

    public override void UseFuel(int quantity)
    {
        base.UseFuel(quantity);
        ore.SubstractFromOre(quantity);
    }

    public void ChooseOre()
    {
        if(ore)
        ore.HidePipe();

        List<Transform> ores = planet.GetComponent<Planet>().ores;

        if (oreIndex <= ores.Count - 1)
        {
            ore = ores[oreIndex].GetComponent<Ore>();
            switchButton.interactable = true;
            signalEnd = ore.signalEnd;
            signal.SetPosition(1, transform.InverseTransformPoint(signalEnd.position));
            ore.ShowPipe();

            GameManager.virtualCamera.Follow = ore.transform;

            oreIndex++;
        }
        else
        {
            ore = null;
            switchButton.interactable = false;
            signalEnd = signalStart;
            signal.SetPosition(1, transform.InverseTransformPoint(signalEnd.position));
            GameManager.virtualCamera.Follow = player.transform;
            oreIndex = 0;
        }
    }

    public void SpawnBox()
    {
        GameObject tempBox = boxPrefab;
        tempBox.GetComponent<Box>().type = ore.oreType;
        GameObject box = Instantiate(tempBox, boxSpawner);
        box.GetComponent<Rigidbody2D>().AddForce(transform.right*100);
    }


    public void SwitchMining()
    {
        if(!isWorking)
        {
            StartCoroutine(SubstractFuel());
            isWorking = true;
            switchButton.GetComponentInChildren<TMP_Text>().text = "STOP";
        }
        else
        {
            StopAllCoroutines();
            isWorking = false;
            switchButton.GetComponentInChildren<TMP_Text>().text = "START";
        }

        animator.SetBool("Working", isWorking);
    }
}
