using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPlayer : Character
{
    public NavMeshAgent agent;
    public BrigeStackManager BrickStackM;
    public Animator animAI;
    Brick targetBrick;
    Transform trans;
    public bool isActive = false;
    
    public void Init()
    {
        this.isPlayer=false;
        isActive = true;
        trans = transform;
        StartCoroutine(CheckStart());
        this.Move();


    }
    public void Move()
    {

        if (GenBrick.instance.stackBrickAi1.Count > 1)
        {
            targetBrick = GenBrick.instance.stackBrickAi1.Pop();
            agent.destination = targetBrick.transform.position;
        }
    }
    public void ActionUpdate()
    {
        if (isActive)
        {
            animAI.SetFloat(Config.ANIM_VELOCITY, agent.velocity.magnitude);
            if ((trans.position - targetBrick.transform.position).sqrMagnitude < Config.FLOAT_RANGE_CHECK)
            {
                this.Move();
            }
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
        agent.isStopped=true;
        
    }
}
