using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour, IDamagable
{
    public Transform rightHand;
    public Transform enemy;
    [SerializeField] int damageAmount = 10;
    [SerializeField] GameObject body;
    private Renderer bodyRenderer;
    [SerializeField] GameObject helmet;
    private Renderer helmetRenderer;
    [SerializeField] GameObject sword;
    private Renderer swordRenderer;
    [SerializeField] int maxHealth = 100;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemyLayer;
    public Animator animator;
    CharacterController characterController;
    int isDeadHash;
    public delegate void DinoAnim(int animIndex);  //delegate that allows animation to change 
    public static event DinoAnim dinoAnimInfo;
    public delegate void PlayerDead(bool playerIsDead);  
    public static event PlayerDead playerIsDeadInfo;
    private const int PLAYER_DEATH_OVERRIDE_INDEX = 10;



    public int Health { get; set; }

    public void Damage()
    {
        bool isDead = animator.GetBool(isDeadHash);
        if (!isDead)
        {
            animator.SetTrigger("Hit");
            Health -= damageAmount;
            if (Health <= 0)
            {
                Die();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = maxHealth;
        bodyRenderer = body.GetComponent<SkinnedMeshRenderer>();
        helmetRenderer = helmet.GetComponent<SkinnedMeshRenderer>();
        swordRenderer = sword.GetComponent<MeshRenderer>();
        characterController = GetComponent<CharacterController>();
        isDeadHash = Animator.StringToHash("IsDead");
        animator.SetBool(isDeadHash, false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetTrigger("Dance");
        }
    }

    void Die()
    {
        //TODO:
        //play die animation
        bool isDead = animator.GetBool(isDeadHash);

        if (!isDead)
            animator.SetTrigger("Dead");
        // characterController.Move(new Vector3(transform.position.x, 0f, transform.position.z));

        //disable player
        
        animator.SetBool(isDeadHash, true);
        playerIsDeadInfo(true);
        //trigger dinosaur victory animation
        dinoAnimInfo(10);




        Debug.Log("Player has died");
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void Attack()
    {
        //play attack animation
        animator.SetTrigger("Attack");
        Debug.Log("Player attack");
        Collider[] hitEnemy = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemy)
        {
            if (enemy.TryGetComponent(out IDamagable hit))
            {
                Debug.Log(enemy.name + " hit");
                hit.Damage();
            }
            
        }
    }
}
