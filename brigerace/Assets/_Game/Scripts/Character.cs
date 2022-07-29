using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public bool isFall = false;
    public bool isPlayer=true;
    public Animator charAnimator;
    public void SetAnimChar(AnimState state)
    {
        switch (state)
        {
            case AnimState.IDLE:
                charAnimator.Play(Config.ANIM_IDLE);
                charAnimator.SetBool(Config.ANIM_FALL, false);
                charAnimator.SetFloat(Config.ANIM_VELOCITY, 0);
                break;
            case AnimState.MOVE:
                charAnimator.Play(Config.ANIM_MOVE);
                charAnimator.SetFloat(Config.ANIM_VELOCITY, 0.2f);
                break;
            case AnimState.FALL:
                charAnimator.Play(Config.ANIM_FALL);
                charAnimator.SetBool(Config.ANIM_FALL, true);
                break;
        }

    }
    public virtual void Fall()
    {
        isFall=true;
    }
}
