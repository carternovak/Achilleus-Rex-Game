using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float lookSensitivity = 5;
    [SerializeField] private float jumpHeight = 10;
    [SerializeField] private float gravity = 9.81f;

    private Vector2 moveVector;
    private Vector2 lookVector;
    private Vector3 rotation;
    private float verticalVelocity;
    private CharacterController characterController;
    private Animator animator;

    private Vector3 moveDir = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        Move();
        //Run();
        Rotate();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        //moveVector = context.ReadValue<Vector2>();
        if(context.ReadValueAsButton())
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
            //moveVector = context.ReadValue<Vector2>();
        }
    }

    /*private void Run()
    {
        verticalVelocity += -gravity*Time.deltaTime;

        if(characterController.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -0.1f*gravity*Time.deltaTime;
        }

        Vector3 move = transform.right*moveVector.x + transform.forward*moveVector.y + transform.up*verticalVelocity;
        characterController.Move(move*moveSpeed*Time.deltaTime);
    }*/

    
    public void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
        if(moveVector.magnitude > 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        moveDir = new Vector3(moveVector.x, 0, moveVector.y);
        if (moveDir.magnitude > 1.0f)
        {
            moveDir = moveDir.normalized;
        }
        moveDir = transform.InverseTransformDirection(moveDir);
        Debug.Log(moveDir);
        transform.LookAt(moveDir);

    }

    private void Move()
    {
        verticalVelocity += -gravity*Time.deltaTime;

        if(characterController.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -0.1f*gravity*Time.deltaTime;
        }

        Vector3 move = transform.right*moveVector.x + transform.forward*moveVector.y + transform.up*verticalVelocity;
        characterController.Move(move*moveSpeed*Time.deltaTime);
        
        //transform.rotation = Quaternion.LookRotation(move);

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookVector = context.ReadValue<Vector2>();
/*         Vector3 currPosition = transform.position;
        Vector3 newPos = new Vector3(moveVector.x, 0, moveVector.y);
        Vector3 posToLookAt = currPosition + newPos; */

        //transform.LookAt(posToLookAt);
    }

    public void Rotate()
    {
        rotation.y += lookVector.x*lookSensitivity*Time.deltaTime;
        transform.localEulerAngles = rotation;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(characterController.isGrounded && context.performed)
        {
            animator.Play("Jump");
            //Jump();
        }
    }

    private void Jump()
    {
        verticalVelocity = Mathf.Sqrt(jumpHeight*gravity);
    }
}
