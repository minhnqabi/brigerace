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
    public Transform moveCheck;
    bool _isInGround, _isMoveFoward;
    public LayerMask groundMask;
    public LayerMask brigeMask;
    public int brickNum;
    private void Awake()
    {
        trans = transform;
        ManageUI.instance.UpdateBrickCount(this.brickNum);
    }

    private void Update()
    {

        if (moveCheck != null) _isMoveFoward = Physics.Raycast(moveCheck.position, Vector3.down, 10f, groundMask);
        RaycastHit inf;
        if (Physics.Raycast(moveCheck.position, Vector3.down, out inf, 10f, brigeMask))
        {
            ActiveStep(inf.transform.parent.GetComponent<Brige>());
            inf.transform.gameObject.SetActive(false);
        }
        if (joystick.inputVector != Vector2.zero)
        {
            _moveDirection = (transform.right * joystick.Horizontal + transform.forward * joystick.Vertical).normalized;
            float _speed = _isMoveFoward ? _spd : 0;
            controller.Move(_moveDirection * _speed * Time.deltaTime * joystick.rate);

            _moveRotate = Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg;
            tran_Rotate.localRotation = Quaternion.Euler(new Vector3(0, _moveRotate, 0));

        }
        anim.SetFloat("velocity", joystick.inputVector.magnitude);
    }
    public void ActiveStep(Brige br)
    {
        if (brickNum > 0)
        {
            brickNum--;
            br.ActiveBrige();
            ManageUI.instance.UpdateBrickCount(brickNum);

        }
    }
    public void AddBrick()
    {
      brickNum++;
      ManageUI.instance.UpdateBrickCount(brickNum);
    }
}
