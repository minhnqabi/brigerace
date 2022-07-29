using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{

    public Joystick joystick;
    Vector3 _moveDirection = Vector3.zero;
    public CharacterController controller;
    public Transform tran_Rotate;
    public float _spd, _moveRotate;
    public Animator anim;
    Transform trans;
    public Transform moveCheck, groundCheck;
    bool _isInGround, _isMoveFoward;
    public LayerMask groundMask;
    public LayerMask brigeMask;
    public int brickNum;
    Vector3 velocity;
    float _gravity = -9.8f;
    public BrigeStackManager brickM;
    private void Awake()
    {
        trans = transform;

    }
    private void Start()
    {
        ManageUI.instance.UpdateBrickCount(brickM.allBrick.Count);
    }

    private void Update()
    {
        if (!this.isFall)
        {
            if (groundCheck != null) _isInGround = this.GroundCheck();
            if (moveCheck != null) _isMoveFoward = this.MoveCheck(groundMask);
            if (_isInGround && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            RaycastHit inf;
            if (Physics.Raycast(moveCheck.position, Vector3.down, out inf, Config.FLOAT_MOVE_CHECK, brigeMask))
            {
                BrigeDetect brigeD = inf.transform.gameObject.GetComponent<BrigeDetect>();
                if (brigeD)
                {

                    ActiveStep(brigeD.brige);
                    //inf.transform.gameObject.SetActive(false);
                }
            }
            if ((joystick.inputVector - Vector2.zero).sqrMagnitude > 0.1f)
            {
                _moveDirection = (transform.right * joystick.Horizontal + transform.forward * joystick.Vertical).normalized;
                float _speed = _isMoveFoward ? _spd : 0;

                controller.Move(_moveDirection * _speed * Time.deltaTime * joystick.rate);
                _moveRotate = Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg;
                tran_Rotate.localRotation = Quaternion.Euler(new Vector3(0, _moveRotate, 0));
                anim.SetFloat(Config.ANIM_VELOCITY, joystick.inputVector.magnitude);
                velocity.y += _gravity * Time.deltaTime * 2;
                controller.Move(velocity * Time.deltaTime);
            }
            else
            {
                anim.SetFloat(Config.ANIM_VELOCITY, 0);
            }
        }

    }
    public void ActiveStep(Brige br)
    {
        if (brickM.allBrick.Count > 0)
        {
            brickM.UseBrick();
            br.ActiveBrige(brickM);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Config.TAG_GATE_LV2))
        {
            GenBrick.instance.AddOwnerToLv2(StepType.PLAYER);
            GameConfig.instance.HandleLv2Start();
        }
        if (other.CompareTag(Config.TAG_PLAYER))
        {
            BrigeStackManager bm = Cache.GenCollectItems(other);
            if (bm.allBrick.Count > this.brickM.allBrick.Count)//nếu ai có nhiều gạch hơn thì player rơi gạch
            {
                this.brickM.FallBrick();
            }
            else
            {
                bm.FallBrick();
            }
            // brickM.FallBrick();
        }
    }
    public bool GroundCheck()
    {
        return Physics.Raycast(groundCheck.position, Vector3.down, Config.FLOAT_GROUND_CHECK, groundMask);
    }
    public bool MoveCheck(LayerMask _mask)
    {
        return Physics.Raycast(moveCheck.position, Vector3.down, Config.FLOAT_MOVE_CHECK, _mask);
    }

}
