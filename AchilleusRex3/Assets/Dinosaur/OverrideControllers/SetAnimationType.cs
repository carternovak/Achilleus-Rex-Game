using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Receiver delegate that will switch animation controllers for the dinosaur based on an Event that has occurred
 * 
 **/
public class SetAnimationType : MonoBehaviour
{
    [SerializeField] private AnimatorOverrideController[] overrideControllers;
    [SerializeField] private DinoAnimatorOverride overrider;

    public void SetAnim(int value)
    {
       
        overrider.SetAnimations(overrideControllers[value]);
    }
    // Start is called before the first frame update
   

    private void OnEnable()
    {
        enemyFollow.dinoAnimInfo += SetAnim;
    }

    private void OnDisable()
    {
        enemyFollow.dinoAnimInfo -= SetAnim;
    }

    

   
}
