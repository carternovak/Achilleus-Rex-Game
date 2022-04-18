using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoAnimatorOverride : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetAnimations(AnimatorOverrideController overrideController)
    {
        _animator.runtimeAnimatorController = overrideController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
