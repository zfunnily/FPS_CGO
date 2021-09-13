using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_MoveMementController : MonoBehaviour
{
    private CharacterController characterController;
    private Animator characterAnimator;
    private Transform characterTransForm;
    private float velocity;
    private Vector3 movementDirection;
    public float MovementSpeed = 2; //移动速度

    public float Gravity = 9.8f;
    public float JumpHeight; //跳跃高度
    public float SprintingSpeed = 8;
    public float WalkSpeed = 4;
    public float SprintingSpeedWhenCrouch = 4;//当下蹲时
    public float WalkSpeedWhenCrouch = 2;//当下蹲时
    public float CrouchHeight = 1f; //下蹲高度
    private bool isCrouched; //是否下蹲
    private float originHeight; //原来的高度

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        // characterForm = GetComponent<Transform>();
        characterAnimator = GetComponentInChildren<Animator>();
        characterTransForm = transform;
        originHeight = characterController.height;
        isCrouched = false;
    }

    void Update()
    {

        float currentSpeed = WalkSpeed;
        //如果在地面 则返回 true
        if (characterController.isGrounded)
        {
            var tmp_Horizontal = Input.GetAxis("Horizontal");
            var tmp_Vertical = Input.GetAxis("Vertical");

            movementDirection =  characterTransForm.TransformDirection(new Vector3(tmp_Horizontal, 0, tmp_Vertical));
            // characterTransForm.LookAt(characterTransForm.position + tmp_MovementDirection);
            // characterController.SimpleMove(movementDirection * Time.deltaTime * MovementSpeed);

            if (Input.GetButtonDown("Jump"))
            {
               movementDirection.y = JumpHeight; 
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                var tmpCrouchHeight = isCrouched?originHeight:CrouchHeight ;
                StartCoroutine(DoCrouch(tmpCrouchHeight));
                isCrouched = !isCrouched;
            }
            //shift奔跑
            if (isCrouched)
            {
                currentSpeed = Input.GetKey(KeyCode.LeftShift) ? SprintingSpeedWhenCrouch : WalkSpeedWhenCrouch;
            }
            else

            {
                currentSpeed = Input.GetKey(KeyCode.LeftShift) ? SprintingSpeed : WalkSpeed;
            }

            var tmp_velocity = characterController.velocity;
            tmp_velocity.y = 0;
            velocity = tmp_velocity.magnitude;
            characterAnimator.SetFloat("Velocity", velocity, 0.25f, Time.deltaTime);
        }
        movementDirection.y -=  Gravity * Time.deltaTime;
        characterController.Move(movementDirection * Time.deltaTime * currentSpeed);//不具备重力算法
        Debug.Log(velocity);

    }

    private IEnumerator DoCrouch(float target)
    {
        float tmp_CurrentHeight = 0;
        while(Mathf.Abs(characterController.height - target) > 0.1f)
        {
            yield return null;
            characterController.height = Mathf.SmoothDamp(characterController.height, target, ref tmp_CurrentHeight, Time.deltaTime * 5 );
        }
    }
}
