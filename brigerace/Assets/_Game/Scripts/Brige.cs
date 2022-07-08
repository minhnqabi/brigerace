using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brige : MonoBehaviour
{
    public StepType brigeType;
    public BrigeDetect brigeDetect;
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
    public void ActiveBrige(BrigeStackManager mng)
    {
        if (!brigeStatusIsActive)
        {
            brigeType=mng.character;
            brigeRender.GetComponent<MeshRenderer>().materials=GameConfig.instance.GetMatByType(brigeType);
            brigeRender.SetActive(true);
            brigeCol.enabled = true;
            brigeDetect.Hide();
        }
    }

}
