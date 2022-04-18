using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class characterMovement : MonoBehaviour
{

    Animator animator;

    int isWalkingHash;
    int isRunningHash;

    PlayerInput input;

    Vector2 currInputVector;
    private Vector2 smoothInputVel;
    [SerializeField] private float smoothInputSpeed = .2f;
    Vector2 currentMovement;
    bool movementPressed;
    bool runPressed;
    bool run2Pressed;
    bool jumpPressed;
    private CharacterController characterController;

    void Awake()
    {
        input = new PlayerInput();

        input.CharacterControls.Walk.performed += ctx => {
            currentMovement = ctx.ReadValue<Vector2>();
            movementPressed = currentMovement.x != 0 || currentMovement.y != 0;
        };
        // Return true if currentMovement has a value (player is attempting to move)
        input.CharacterControls.Run.performed += ctx => {
            currentMovement = ctx.ReadValue<Vector2>();
            runPressed = currentMovement.x != 0 || currentMovement.y != 0;
        };
        
        input.CharacterControls.Run2.performed += ctx => run2Pressed = ctx.ReadValueAsButton();
        // these arent being used because they are not functioning correctly
        input.CharacterControls.Jump.performed += ctx => jumpPressed = ctx.ReadValueAsButton();
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        handleMovement();
        handleRotation();
    }

    void handleRotation()
    {
        Vector3 currPos = transform.position;
        currInputVector = Vector2.SmoothDamp(currInputVector, currentMovement, ref smoothInputVel, smoothInputSpeed);
        Vector3 newPos = new Vector3(currInputVector.x, 0, currInputVector.y);
        Vector3 posToLookAt = currPos + newPos;
        transform.LookAt(posToLookAt);
    }

    void handleMovement()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        //float mag = Mathf.Clamp01(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).sqrMagnitude);
        //Debug.Log(isRunning);
        //Debug.Log(isWalking);
        
        // Issue with movementPressed not being changed
        InputOverride();
        if (movementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        if (!movementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if ((movementPressed && run2Pressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }
        if ((!movementPressed || !run2Pressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
    }

    void InputOverride()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        //Debug.Log(characterController.isGrounded);
        if (Input.GetKeyUp("w"))
        {
            movementPressed = false;
        } 
        else if (Input.GetKeyUp("a"))
        {
            movementPressed = false;
        } 
        else if (Input.GetKeyUp("s"))
        {
            movementPressed = false;
        } 
        else if (Input.GetKeyUp("d"))
        {
            movementPressed = false;
        }

        if (Input.GetKey("w"))
        {
            movementPressed = true;
        } 
        else if (Input.GetKey("a"))
        {
            movementPressed = true;
        } 
        else if (Input.GetKey("s"))
        {
            movementPressed = true;
        } 
        else if (Input.GetKey("d"))
        {
            movementPressed = true;
        }

        if(characterController.isGrounded && (Input.GetKeyDown("space") || Input.GetKeyDown("joystick button 0")) && (isRunning || isWalking))
        {
            animator.Play("Jump");
        } 
        // cant use jumpPressed because it keeps jumping
        // as if jumpPressed turns to true and stays true
        else if(characterController.isGrounded && (Input.GetKeyDown("space") || Input.GetKeyDown("joystick button 0")) && !(isRunning || isWalking))
        {
            animator.Play("StandingJump");
        }
    }
    void OnEnable()
    {
        input.CharacterControls.Enable();
    }

    void OnDisable()
    {
        input.CharacterControls.Disable();
    }

    /* public void OnJump(InputAction.CallbackContext context)
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        if(characterController.isGrounded && context.performed && (isRunning || isWalking))
        {
            animator.Play("Jump");
        } 
        else if(characterController.isGrounded && context.performed && !(isRunning || isWalking))
        {
            animator.Play("StandingJump");
        }
    }

    void Jump()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        if(characterController.isGrounded && (Input.GetKeyDown("space") || Input.GetKeyDown("A")) && (isRunning || isWalking))
        {
            animator.Play("Jump");
        } 
        else if(characterController.isGrounded && (Input.GetKeyDown("space") || Input.GetKeyDown("A")) && !(isRunning || isWalking))
        {
            animator.Play("StandingJump");
        }
    } */

    //keycode. joystick button 0
}
