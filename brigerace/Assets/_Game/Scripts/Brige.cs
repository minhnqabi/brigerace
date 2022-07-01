using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brige : MonoBehaviour
{
    public StepType brigeType;
    public bool brigeStatusIsActive = false;
    public GameObject brigeRender;
    BoxCollider brigeCol;
    private void Awake()
    {
        brigeCol = gameObject.GetComponent<BoxCollider>();
        if (!brigeStatusIsActive)
        {
            brigeCol.enabled = false;
        }
    }
    public void Setup()
    {

    }
    public void ActiveBrige()
    {
        if (!brigeStatusIsActive)
        {

            brigeRender.SetActive(true);
            brigeCol.enabled = true;
        }
    }

}
