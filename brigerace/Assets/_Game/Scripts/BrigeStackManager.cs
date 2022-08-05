using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrigeStackManager : MonoBehaviour
{
    public Character _char;
    public Stack<Brick> allBrick = new Stack<Brick>();
    public Transform trans;
    public float heigh;
    public StepType character;
    public Animator animatorChar;

    public void PushBrick(Brick _br)
    {
        allBrick.Push(_br);
        _br.transform.parent = trans;
        //PlayerController.instance.AddBrick();
        _br.transform.position = trans.position + Vector3.up * heigh * allBrick.Count;
        _br.transform.localRotation = Quaternion.identity;

        //ManageUI.instance.UpdateBrickCount(allBrick.Count);
        if(allBrick.Count>3)
        {
            if(!_char.isPlayer)
            {
                _char.gameObject.GetComponent<AIPlayer>().GoToLevel2();
            }
        }
    }
    public void UseBrick()
    {
        if (allBrick.Count > 0)
        {
            Debug.Log("vao day r");
            allBrick.Pop().Remove();
            // ManageUI.instance.UpdateBrickCount(allBrick.Count);
        }
        else 
        {
            Debug.Log("Back to collect1");
            if(!_char.isPlayer)
            {
                Debug.Log("Back to collect");
                _char.GetComponent<AIPlayer>().BackToCollect();
            }
        }
    }
    public void FallBrick()
    {
        _char.Fall();
        _char.SetAnimChar(AnimState.FALL);
        for (int i = 0; i < allBrick.Count; i++)
        {
            allBrick.Pop().FallBrick();
        }
        StartCoroutine(ReturnDefault());
    }
    IEnumerator ReturnDefault()
    {
        yield return new WaitForSeconds(3.0f);

        _char.SetAnimChar(AnimState.IDLE);
        _char.isFall = false;
        if (!_char.isPlayer)
        {
            _char.gameObject.GetComponent<AIPlayer>().agent.isStopped = false;
        }
    }



}
