using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtNearby : MonoBehaviour
{
    public Transform headTransform;
    public Transform aimTargetTransform;
    public PointOfInterest pointOfInterest;

    public Vector3 origin;
    public float visionRadius;
    public float lerpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //origin = aimTargetTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        origin = aimTargetTransform.position;
        Collider[] cols = Physics.OverlapSphere(headTransform.position + transform.forward, visionRadius);

        pointOfInterest = null;

        foreach (Collider col in cols) {
            //Debug.Log(col.GetComponent<PointOfInterest>());
            if (col.GetComponent<PointOfInterest>() != null)
            {
                //Debug.Log(col.GetComponent<PointOfInterest>());
                pointOfInterest = col.GetComponent<PointOfInterest>();
                break;
            }
        }
        
        Vector3 targetPosition;
        //Debug.Log(pointOfInterest);
        if (pointOfInterest != null)
        {
            //GameObject.Find("AimTarget").SetActive(true);
            targetPosition = pointOfInterest.GetLookTarget().position; 
        }
        else
        {
            //GameObject.Find("AimTarget").SetActive(false);
            targetPosition = origin;
        }

        float speed = lerpSpeed / 10;
        aimTargetTransform.position = Vector3.Lerp(aimTargetTransform.position, targetPosition, Time.deltaTime * speed);
    }

}
