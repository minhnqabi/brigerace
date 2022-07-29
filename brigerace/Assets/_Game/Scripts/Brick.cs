using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public StepType owner;
    public Material[] player, mat1, mat2, none;
    public MeshRenderer brickMesh;
    public BoxCollider col;
    bool isLv1;
    public Transform container;//lưu transform brick lấy từ đâu 

    public void Setup(Vector3 pos, StepType _owner, Transform _tr = null, bool _isLv1 = true)
    {
        col.enabled = true;
        isLv1 = _isLv1;
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
    public Material[] GetMatByOwner(StepType _s)
    {
        switch (_s)
        {
            case StepType.NONE:
                return none;

            case StepType.PLAYER:
                return player;

            case StepType.AI1:
                return mat1;

            case StepType.AI2:
                return mat2;


        }
        return none;
    }
    public void Remove()
    {
        transform.parent = null;
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
        //BrigeStackManager stackM = other.GetComponent<BrigeStackManager>();
        BrigeStackManager stackM = Cache.GenCollectItems(other);
        if (stackM)
        {
            if (stackM.character == this.owner || this.owner == StepType.NONE)
            {
                //Debug.Log("vao day r");
                stackM.PushBrick(this);
                col.enabled = false;
                if (container)
                {
                    GenBrick.instance.PushTransToStackBrickNull(container, isLv1);
                }
                if (this.owner == StepType.NONE)
                {
                    brickMesh.materials = this.GetMatByOwner(stackM.character);
                }

            }
        }
    }
    public void FallBrick()
    {
        groundCheck.canCheck = true;
        transform.position = new Vector3(transform.position.x * Random.Range(-1.5f, 1.5f), transform.position.y, transform.position.z * Random.Range(-1.5f, 1.54f));
        canFall = true;
        this.owner = StepType.NONE;
        brickMesh.materials = none;
        transform.SetParent(GameConfig.instance.nonBrickContainer);

    }
    bool canFall = false;

    public GroundCheck groundCheck;
    public float jumpForce = 20;
    public float gravity = -5.81f;
    public float gravityScale = 0.5f;
    float velocity;
    private void Update()
    {
        if (canFall)
        {
            //Debug.Log("fall");
            velocity += gravity * gravityScale * Time.deltaTime;
            if (groundCheck.isGrounded && velocity < 0)
            {

                float offset = 0.001f;
                velocity = 0;
                Vector3 closestPoint = groundCheck.hit.collider.ClosestPoint(transform.position);
                Vector3 snappedPosition = new Vector3(transform.position.x, closestPoint.y + offset, transform.position.z);

                transform.position = snappedPosition;
                canFall = false;
                col.enabled = true;

            }

            transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);
        }

    }

}


