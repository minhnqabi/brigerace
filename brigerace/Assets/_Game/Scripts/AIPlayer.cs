using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPlayer : Character
{
    public GenBrige aiBrige;
    public NavMeshAgent agent;
    public BrigeStackManager BrickStackM;
    public Animator animAI;
    Brick targetBrick;
    Transform trans;
    public bool isActive = false;
    public AIState _state;
    bool _isInGround;
    public Transform groundCheck, moveCheck;
    public LayerMask groundMask;
    public LayerMask brigeMask;

    public void Init()
    {
        this.isPlayer = false;
        isActive = true;
        trans = transform;
        StartCoroutine(CheckStart());
        this._state = AIState.COLLECT;
        this.Move();


    }
    public void Move()
    {
        
        if (GenBrick.instance.stackBrickAi1.Count > 1)
        {
            agent.isStopped=false;
            targetBrick = GenBrick.instance.stackBrickAi1.Pop();
            agent.destination = targetBrick.transform.position;

        }
    }
    public bool isStop;
    public void ActionUpdate()
    {
        if (isActive)
        {

            RaycastHit inf;

            if (Physics.Raycast(moveCheck.position, Vector3.down, out inf, Config.FLOAT_MOVE_CHECK, brigeMask))
            {
                //                Debug.Log("Ai brige");
                BrigeDetect brigeD = inf.transform.gameObject.GetComponent<BrigeDetect>();
                if (brigeD)
                {
                    //Debug.Log("Ai brige1");

                    ActiveStep(brigeD.brige);
                    //inf.transform.gameObject.SetActive(false);
                }
            }

            if (_state == AIState.COLLECT)
            {
                animAI.SetFloat(Config.ANIM_VELOCITY, agent.velocity.magnitude);
                if ((trans.position - targetBrick.transform.position).sqrMagnitude < Config.FLOAT_RANGE_CHECK)
                {
                    this.Move();
                }
            }
            if (_state == AIState.MOVENEXT)
            {

                if (groundCheck != null) _isInGround = this.GroundCheck();
                agent.isStopped = !_isInGround;
                isStop = agent.isStopped;
            }

        }

    }

    public void ActiveStep(Brige br)
    {
        if (BrickStackM.allBrick.Count > 0)
        {
            BrickStackM.UseBrick();
            br.ActiveBrige(BrickStackM);
        }
        else
        {
            this.BackToCollect();
        }
    }
    IEnumerator CheckStart()
    {
        yield return null;
        while (GenBrick.instance.stackBrickAi1.Count <= 1)
        {
            this.Move();
            yield return null;

        }
    }

    public override void Fall()
    {
        base.Fall();
        agent.isStopped = true;

    }
    public void GoActiveBrige()
    {
        if (this.BrickStackM.allBrick.Count > 0)
        {
            if (this.aiBrige.CurrentActiveBrige())
            {
                agent.destination = this.GetClosesNavmeshPoint(this.aiBrige.CurrentActiveBrige().transform.position);
            }
            else
            {
                agent.destination = aiBrige.transform.position;
            }

        }
        else
        {
            this.Move();
        }
    }
    public Vector3 GetClosesNavmeshPoint(Vector3 pos)
    {
        NavMeshHit closes;
        if (NavMesh.SamplePosition(pos, out closes,
                Config.FLOAT_RANGE_CHECK, 1 << NavMesh.GetAreaFromName("Walkable")))
        {
            return closes.position;
        }
        else
        {
            return pos;
        }
    }
    public Transform lv2;
    public void GoToLevel2()
    {
        this._state = AIState.MOVENEXT;
        agent.destination = lv2.position;
    }
    public float groundFloatCheck = 0.1f;
    public bool GroundCheck()
    {
        return Physics.Raycast(groundCheck.position, Vector3.down, groundFloatCheck, groundMask);
    }
    public void BackToCollect()
    {
        this._state = AIState.COLLECT;
        agent.Warp(this.GetClosesNavmeshPoint(trans.position));
        agent.isStopped = false;

        this.Move();
    }
}
