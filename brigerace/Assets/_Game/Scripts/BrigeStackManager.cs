using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrigeStackManager : MonoBehaviour
{
    public Stack<Brick> allBrick = new Stack<Brick>();
    public Transform trans;
    public float heigh;
    public StepType character;

    public void PushBrick(Brick _br)
    {
        allBrick.Push(_br);
        _br.transform.parent = trans;
        //PlayerController.instance.AddBrick();
        _br.transform.position = trans.position + Vector3.up * heigh * allBrick.Count;
        _br.transform.localRotation=Quaternion.identity;
        ManageUI.instance.UpdateBrickCount(allBrick.Count);
    }
    public void UseBrick()
    {
        if (allBrick.Count > 0)
        {
            allBrick.Pop().Remove();
            ManageUI.instance.UpdateBrickCount(allBrick.Count);
        }
    }



}
