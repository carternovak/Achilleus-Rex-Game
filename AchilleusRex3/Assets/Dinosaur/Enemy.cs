using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected abstract void Update();
    
}
