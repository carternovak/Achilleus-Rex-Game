using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    public delegate void DinoFacingPlayer(bool bite);  
    public static event DinoFacingPlayer dinoFacingPlayerInfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
           // Debug.Log("Dino can bite");
            dinoFacingPlayerInfo(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            //Debug.Log("Dino can no longer bite");
            dinoFacingPlayerInfo(false);
        }
    }
       
}
