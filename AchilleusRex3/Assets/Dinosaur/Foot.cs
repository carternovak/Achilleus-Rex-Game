using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{
    public Transform[] FootTarget;
    public Transform LeftBigToe;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //FootAdjust();
    }

    public void LateUpdate()
    {
        FootAdjust();
       // BigToeTapper();
    }

    void FootAdjust()
    {
        for (int i = 0; i < FootTarget.Length; i++)
        {
            var foot = FootTarget[i];
            var ray = new Ray(foot.transform.position + Vector3.up * 0.5f, Vector3.down);
            var hitInfo = new RaycastHit();
            if (Physics.SphereCast(ray, 0.05f, out hitInfo, 0.50f))
                foot.position = hitInfo.point + Vector3.up * 0.05f;
        }
    }

    void BigToeTapper()
    {
        var toe = LeftBigToe;
        toe.position = new Vector3(0,.1f,0);
    }
}
