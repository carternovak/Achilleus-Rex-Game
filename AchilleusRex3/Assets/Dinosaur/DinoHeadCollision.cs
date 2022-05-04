using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoHeadCollision : MonoBehaviour
{
    private SphereCollider sphereCollider;

    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Player collided with dino head");
           // if (collision == GameObject.FindGameObjectWithTag("Player").GetComponent<IDamageable>() )

        }
    }
}
