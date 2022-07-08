using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonMonoBehaviour<PlayerController>
{

    public Joystick joystick;
    Vector3 _moveDirection = Vector3.zero;
    public CharacterController controller;
    public Transform tran_Rotate;
    public float _spd, _moveRotate;
    public Animator anim;
    Transform trans;
    public Transform moveCheck,groundCheck;
    bool _isInGround, _isMoveFoward;
    public LayerMask groundMask;
    public LayerMask brigeMask;
    public int brickNum;
    Vector3 velocity;
    float _gravity = -9.8f;
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
        if (groundCheck != null) _isInGround = Physics.Raycast(groundCheck.position, Vector3.down, 0.05f, groundMask);
        if (moveCheck != null) _isMoveFoward = Physics.Raycast(moveCheck.position, Vector3.down, 10f, groundMask);
        if (_isInGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        RaycastHit inf;
        if (Physics.Raycast(moveCheck.position, Vector3.down, out inf, 10f, brigeMask))
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

        }
        anim.SetFloat(Config.ANIM_VELOCITY, joystick.inputVector.magnitude);
        velocity.y += _gravity * Time.deltaTime * 2;
        controller.Move(velocity * Time.deltaTime);
    }
    public void ActiveStep(Brige br)
    {
        if (brickM.allBrick.Count > 0)
        {
            brickM.UseBrick();
            br.ActiveBrige(brickM);
        }
    }
    public BrigeStackManager brickM;
    
}
