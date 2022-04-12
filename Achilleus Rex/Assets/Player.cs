using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
<<<<<<< Updated upstream
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
=======
    public CharacterController controller;

    CameraController cameraController;
    public Transform cam;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    PlayerControls inputActions;

    public bool lockOnInput;
    public bool lockedOn;
    public bool rightStickRightInput;
    public bool rightStickLeftInput;

    public void OnEnable()
    {
        if(inputActions == null)
        {
            inputActions = new PlayerControls();
            inputActions.Gameplay.LockOn.performed += i => lockOnInput = true;
            inputActions.Gameplay.LockOnTargetRight.performed += i => rightStickRightInput = true;
            inputActions.Gameplay.LockOnTargetLeft.performed += i => rightStickLeftInput = true;
        }

        inputActions.Enable();
    }

    private void Awake()
    {
        //cameraController = CameraController.singleton;
        cameraController = FindObjectOfType<CameraController>();
    }

    private void FixedUpdate()
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
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
=======
        if (direction.magnitude >= 0.1f)
>>>>>>> Stashed changes
        {
            animator.SetBool("isWalking", false);
        }

        moveDir = new Vector3(moveVector.x, 0, moveVector.y);
        if (moveDir.magnitude > 1.0f)
        {
            moveDir = moveDir.normalized;
        }
<<<<<<< Updated upstream
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
=======

        float horizontalMouse = Input.GetAxisRaw("Mouse X");
        float verticalMouse = Input.GetAxisRaw("Mouse Y");
        //Debug.Log(horizontalMouse + verticalMouse);
        float delta = Time.fixedDeltaTime;
        if (cameraController != null)
        {
            cameraController.FollowTarget(delta);
            cameraController.HandleCameraRotation(delta, horizontalMouse, verticalMouse);
        }

        HandleLockOnInput();
        //cameraController.HandleLockOn();
    }

    private void HandleLockOnInput()
    {
        Debug.Log("Lock On Controls Online");
        if (lockOnInput && lockedOn == false)
        {
            Debug.Log("Locking");
            lockOnInput = false;
            cameraController.HandleLockOn();
            if (cameraController.nearestLockOnTarget != null)
            {
                cameraController.currentLockOnTarget = cameraController.nearestLockOnTarget;
                cameraController.crossHair.SetActive(true);
                lockedOn = true;
            }
        }
        else if (lockOnInput && lockedOn)
        {
            Debug.Log("De-Locking");
            lockOnInput = false;
            cameraController.crossHair.SetActive(false);
            lockedOn = false;
            cameraController.ClearLockOnTargets();
        }

        if (lockedOn)
        {
            cameraController.crossHair.transform.position = Camera.main.WorldToScreenPoint(cameraController.currentLockOnTarget.transform.position);
            //DEBUG
            if(cameraController.rightLockTarget != null)
            {
                cameraController.rightTargetCrossHair.SetActive(true);
                cameraController.rightTargetCrossHair.transform.position = Camera.main.WorldToScreenPoint(cameraController.rightLockTarget.transform.position);
            } else
            {
                cameraController.rightTargetCrossHair.SetActive(false);
            }
            if(cameraController.leftLockTarget != null)
            {
                cameraController.leftTargetCrossHair.SetActive(true);
                cameraController.leftTargetCrossHair.transform.position = Camera.main.WorldToScreenPoint(cameraController.leftLockTarget.transform.position);
            } else
            {
                cameraController.leftTargetCrossHair.SetActive(false);
            }
            
            if (rightStickLeftInput) //If user is locked on and flicks stick to left
            {
                Debug.Log("SWITCHING LEFT TARGET");
                rightStickLeftInput = false;
                cameraController.HandleLockOn();
                if (cameraController.leftLockTarget != null)
                {
                    cameraController.currentLockOnTarget = cameraController.leftLockTarget;
                }
                //cameraController.HandleLockOn();
            }
            else if (rightStickRightInput) //If user is locked on and flicks stick to right
            {
                Debug.Log("SWITCHING RIGHT TARGET");
                rightStickRightInput = false;
                cameraController.HandleLockOn();
                if (cameraController.rightLockTarget != null)
                {
                    cameraController.currentLockOnTarget = cameraController.rightLockTarget;
                }
                //cameraController.HandleLockOn();
            }
        }
        
        

        cameraController.SetCameraHeight();
>>>>>>> Stashed changes
    }
}
