using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public StepType owner;
    public Material[] player, mat1, mat2, none;
    public MeshRenderer brickMesh;
    BoxCollider col;
    public Transform container;//lưu transform brick lấy từ đâu 
    private void Start()
    {

        col = gameObject.GetComponent<BoxCollider>();
    }
    public void Setup(Vector3 pos, StepType _owner, Transform _tr = null)
    {
        if (_tr)
        {
            this.container = _tr;
        }
        transform.position = pos;
        owner = _owner;
        switch (owner)
        {
            case StepType.NONE:
                brickMesh.materials = none;
                break;
            case StepType.PLAYER:
                brickMesh.materials = player;
                break;
            case StepType.AI1:
                brickMesh.materials = mat1;
                break;
            case StepType.AI2:
                brickMesh.materials = mat2;
                break;

        }

    }
    public void Remove()
    {
        transform.parent=null;
        SimplePool.Despawn(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        // if(other.CompareTag(Config.TAG_PLAYER)&&owner==StepType.PLAYER)
        // {
        //     Debug.LogWarning("vao day r");
        //     BrigeStackManager stackM=other.GetComponent<BrigeStackManager>();
        //     if(stackM)
        //     {
        //         stackM.PushBrick(this);
        //         gameObject.GetComponent<BoxCollider>().enabled=false;
        //         //PlayerController.instance.AddBrick();

        //     }
        // }
        BrigeStackManager stackM = other.GetComponent<BrigeStackManager>();
        if (stackM)
        {
            if (stackM.character == this.owner || this.owner == StepType.NONE)
            {
                stackM.PushBrick(this);
                col.enabled = false;
                if(container)
                {
                    GenBrick.instance.PushTransToStackBrickNull(container);
                }

            }
        }
    }
}
