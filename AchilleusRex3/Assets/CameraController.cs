using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Player player;

    public Transform target;
    public Transform camera;
    public Transform cameraPivot;
    private Transform myTransform;
    private Vector3 cameraTransformPosition;

    public float lookSpeed = 0.1f;
    public float followSpeed = 0.1f;
    public float pivotSpeed = 0.03f;

    private float defaultPosition;
    private float lookAngle;
    private float pivotAngle;
    public float minimumPivot = -35;
    public float maximumPivot = 35;

    public float lockedPivotPosition = 2.25f;
    public float unlockedPivotPosition = 1.65f;

    //Camera Lockon to Enemies
    public CharacterLock currentLockOnTarget;
    List<CharacterLock> availableTargets = new List<CharacterLock>();
    public CharacterLock nearestLockOnTarget;
    public CharacterLock leftLockTarget;
    public CharacterLock rightLockTarget;
    public float maximumLockOnDistance = 30;

    public GameObject crossHair;
    public GameObject leftTargetCrossHair;
    public GameObject rightTargetCrossHair;

    private void Awake()
    {
        myTransform = transform;
        defaultPosition = camera.localPosition.z;
        player = FindObjectOfType<Player>();
    }

    public void FollowTarget(float delta)
    {
        Vector3 targetPosition = Vector3.Lerp(myTransform.position, target.position, delta / followSpeed);
        myTransform.position = targetPosition;
    }

    public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
    {
        if (player.lockedOn == false && currentLockOnTarget == null)
        {
            lookAngle += (mouseXInput * lookSpeed) / delta;
            pivotAngle -= (mouseYInput * pivotSpeed) / delta;
            pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

            Vector3 rotation = Vector3.zero;
            rotation.y = lookAngle;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            myTransform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = pivotAngle;

            targetRotation = Quaternion.Euler(rotation);
            cameraPivot.localRotation = targetRotation;
        }
        else
        {
            float velocity = 0;

            Vector3 dir = currentLockOnTarget.lockOnTransform.transform.position - transform.position;
            dir.Normalize();
            dir.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = targetRotation;

            dir = currentLockOnTarget.lockOnTransform.transform.position - cameraPivot.position;
            dir.Normalize();

            targetRotation = Quaternion.LookRotation(dir);
            Vector3 eulerAngle = targetRotation.eulerAngles;
            eulerAngle.y = 0;
            cameraPivot.localEulerAngles = eulerAngle;
        }
    }

    public void HandleLockOn()
    {
        float shortestDistance = Mathf.Infinity;
        float shortestDistanceOfLeftTarget = -Mathf.Infinity;
        float shortestDistanceOfRightTarget = Mathf.Infinity;

        Collider[] colliders = Physics.OverlapSphere(target.position, 26);

        for(int i=0; i< colliders.Length; i++)
        {
            CharacterLock character = colliders[i].GetComponent<CharacterLock>();
            

            if(character != null)
            {
                Vector3 lockTargetDirection = character.transform.position - target.position;
                float distanceFromTarget = Vector3.Distance(target.position, character.transform.position);
                float viewableAngle = Vector3.Angle(lockTargetDirection, camera.forward);

                if (character.lockOnTransform.transform.root != target.transform.root && viewableAngle > -90 && viewableAngle < 90 && distanceFromTarget <= maximumLockOnDistance)
                {
                    availableTargets.Add(character);
                    Debug.Log("Available Target Found!");
                }
            } 
        }

        for (int j=0; j<availableTargets.Count; j++)
        {
            float distanceFromTarget = Vector3.Distance(target.position, availableTargets[j].transform.position);

            if(distanceFromTarget < shortestDistance)
            {
                shortestDistance = distanceFromTarget;
                nearestLockOnTarget = availableTargets[j];
            }

            if (player.lockedOn)
            {
                //Vector3 relativeEnemyPosition = currentLockOnTarget.transform.InverseTransformPoint(availableTargets[j].transform.position);
                //var distanceFromLeftTarget = currentLockOnTarget.transform.position.x - availableTargets[j].transform.position.x;
                //var distanceFromRightTarget = currentLockOnTarget.transform.position.x + availableTargets[j].transform.position.x;

                Vector3 relativeEnemyPosition = camera.transform.InverseTransformPoint(availableTargets[j].transform.position); //Debug.Log(relativeEnemyPosition+availableTargets[j].name);
                var distanceFromLeftTarget = relativeEnemyPosition.x;
                var distanceFromRightTarget = relativeEnemyPosition.x;

                // if enemy is to the left
                if (relativeEnemyPosition.x <= 0.00 && distanceFromLeftTarget > shortestDistanceOfLeftTarget && availableTargets[j] != currentLockOnTarget)
                {
                    shortestDistanceOfLeftTarget = distanceFromLeftTarget;
                    leftLockTarget = availableTargets[j];
                }

                // if enemy is to the right
                else if (relativeEnemyPosition.x >= 0.00 && distanceFromRightTarget < shortestDistanceOfRightTarget && availableTargets[j] != currentLockOnTarget)
                {
                    shortestDistanceOfRightTarget = distanceFromRightTarget;
                    rightLockTarget = availableTargets[j];
                }
            }
        }
    }

    public void ClearLockOnTargets()
    {
        availableTargets.Clear();
        nearestLockOnTarget = null;
        currentLockOnTarget = null;

        leftLockTarget = null;
        rightLockTarget = null;

        //debug
        //leftTargetCrossHair.SetActive(false);
        //rightTargetCrossHair.SetActive(false);
    }

    public void SetCameraHeight()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 newLockedPosition = new Vector3(0, lockedPivotPosition);
        Vector3 newUnlockedPosition = new Vector3(0, unlockedPivotPosition);

        if(currentLockOnTarget != null)
        {
            cameraPivot.transform.localPosition = Vector3.SmoothDamp(cameraPivot.transform.localPosition, newLockedPosition, ref velocity, Time.deltaTime);
        }
        else
        {
            cameraPivot.transform.localPosition = Vector3.SmoothDamp(cameraPivot.transform.localPosition, newUnlockedPosition, ref velocity, Time.deltaTime);
        }
    }
}
