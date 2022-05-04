using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinosaur : Enemy, IDamagable
{
    [SerializeField] int damageAmount = 10;
    [SerializeField] int maxHealth = 100;
    public delegate void DinoAnim(int animIndex);  //delegate that allows animation to change 
    public static event DinoAnim dinoAnimInfo;
    private const int HIT_OVERRIDE_INDEX = 8;
    private const int DEATH_OVERRIDE_INDEX = 9;
    private const int DANCE_OVERRIDE_INDEX = 10;
    public delegate void DinoDead(bool dinoIsDead);
    public static event DinoDead dinoIsDeadInfo;
    private bool isDead = false;
    [SerializeField] SwapDinoToGibs dinoGibberScript;

    public int Health { get; set; }

    public void Damage()
    {
        if (!isDead) { 
            dinoAnimInfo(HIT_OVERRIDE_INDEX);
            Health -= damageAmount;
            if (Health <= 0)
            {

                Die();
                isDead = true;
                dinoGibberScript.Explode();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = maxHealth;
    }

    void Die()
    {
        if (!isDead)
        {
            dinoAnimInfo(DEATH_OVERRIDE_INDEX);
        }
        Debug.Log("Enemy has died");
        dinoIsDeadInfo(true);
        isDead = true;
    }

    // Update is called once per frame
   protected override void Update()
    {
       /* if (Input.GetKeyDown(KeyCode.Mouse1) && !isDead)
        {
            Debug.Log("Dino dance");
            dinoAnimInfo(DANCE_OVERRIDE_INDEX);
        } */
    }
}
