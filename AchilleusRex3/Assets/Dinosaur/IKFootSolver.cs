using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootSolver : MonoBehaviour
{
    [SerializeField] LayerMask terrainLayer = default;
    [SerializeField] Transform body = default;
    [SerializeField] IKFootSolver otherFoot = default;
    [SerializeField] float speed = 1;
    [SerializeField] float stepDistance = 4;
    [SerializeField] float stepLength = 4;
    [SerializeField] float stepHeight = 1;
    [SerializeField] Vector3 footOffset = default;
    [SerializeField] Vector3 raycastDirection = Vector3.down;
    //public LayerMask layerMask;
    Animator anim;
    [Range(0, 1f)] 
    public float DistanceToGround;

    float footSpacing;  //distance of the feet from the center of the body.
    Vector3 oldPosition, currentPosition, newPosition;  //intial, current, and new postion of the foot
    Vector3 oldNormal, newNormal, currentNormal;  //used to calculate orientation of the foot so it can be rotated as needed
    float lerp;  //0-1. are we in the process of playing a step animation? 1 or greater means the animation has completed, and the foot is ready to be moved.
    //Vector3 cast = Vector3(0, -2, 0);

    // Start is called before the first frame update
    void Start()
    {
        footSpacing = transform.localPosition.x;  //record the position of the foot relative to the body.
        currentPosition = newPosition = oldPosition = transform.position;  
        currentNormal = newNormal = oldNormal = transform.up;
        lerp = 1;
      //  anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //CalculateNewFootPosition();
    }

    private void LateUpdate()
    {
        CalculateNewFootPosition();
    }

    void CalculateNewFootPosition()
    {
        transform.position = currentPosition;
        transform.up = currentNormal;
        Ray ray = new Ray(body.position + Vector3.up * 0.5f, Vector3.down);//(body.right * footSpacing), Vector3.down); //Ray(body.position, raycastDirection); //+ (body.right * footSpacing), new Vector3(-1, -4, -1));    //Vector3.down);  //cast ray down from body toward the terrain layer

        //left foot placement
        /* RaycastHit hit;
         Ray ray2 = new Ray(anim.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
         if (Physics.Raycast(ray2, out hit, DistanceToGround + 1f, layerMask))
         {
             if (hit.transform.tag == "Walkable")
             {
                 Vector3 footPosition = hit.point;
                 footPosition.y += DistanceToGround;
                 anim.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);

             }
         } */

        if (Physics.Raycast(ray, out RaycastHit info, 10/*distance of raycast*/, terrainLayer.value)) 
        {
            if (Vector3.Distance(newPosition, info.point) > stepDistance && !otherFoot.IsMoving() && lerp >= 1) //make sure other foot isn't moving.
            {
                lerp = 0;  //can play animation.
                int direction = body.InverseTransformPoint(info.point).z > body.InverseTransformPoint(newPosition).z ? 1 : -1;  //calculation new foot position relative to the body.
                //InverseTransformPoint transforms a position from WorldSpace where raycast hit the ground.
                //are the z positions of the raycast and new position of the foot in front of or behind the dinsosaur?
                //Debug.DrawLine(body.position, info.point, Color.green);
                Debug.DrawRay(body.position + Vector3.up * 0.5f , info.point, Color.yellow, 10);
                newPosition = info.point + (direction * stepLength * body.forward) + footOffset;
                newNormal = info.normal;      
            }        
        }

        if (lerp < 1)
        {
            Vector3 tempPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
            tempPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

            currentPosition = tempPosition;
            currentNormal = Vector3.Lerp(oldNormal, newNormal, lerp);
            lerp += Time.deltaTime * speed;
        }
        else
        {
            oldPosition = newPosition;
            oldNormal = newNormal;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(newPosition, .5f);
    }

    //return true if lerp < 1, means foot is moving.
    public bool IsMoving()
    {
        return lerp < 1;
    }

}
