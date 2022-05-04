using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;

    CameraController cameraController;
    public Transform cam;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    PlayerInput inputActions;

    public bool lockOnInput;
    public bool lockedOn;
    public bool rightStickRightInput;
    public bool rightStickLeftInput;
    int isDeadHash;
    bool isDead = false;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        isDeadHash = Animator.StringToHash("IsDead");
    }

    public void OnEnable()
    {
        if(inputActions == null)
        {
            inputActions = new PlayerInput();
            inputActions.CharacterControls.LockOn.performed += i => lockOnInput = true;
            inputActions.CharacterControls.LockOnTargetRight.performed += i => rightStickRightInput = true;
            inputActions.CharacterControls.LockOnTargetLeft.performed += i => rightStickLeftInput = true;
        }

        inputActions.Enable();
    }

    private void Awake()
    {
        //cameraController = CameraController.singleton;
        cameraController = FindObjectOfType<CameraController>();
    }

    private void FixedUpdate()
    {
        isDead = animator.GetBool(isDeadHash);

        if (!isDead)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }


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
            cameraController.crossHair.transform.position = Camera.main.WorldToScreenPoint(cameraController.currentLockOnTarget.lockOnTransform.transform.position);
            //DEBUG
            /*
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
            */
            
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
    }
}
