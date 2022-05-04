using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyFollow : MonoBehaviour
{
    public NavMeshAgent _enemy;
    public Transform _player;
    private Rigidbody rb;
    public float _minFollowDistance = 20f;
    public float _aggroDistance = 10f;
    public float _attackDistance = .001f;
    private Vector2 movement;
    public float moveSpeed = 5f;
    //private Animator _animator;
    private AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    [SerializeField] float aggroDistanceVelocity = 5f;
    [SerializeField] float attackDistanceVelocity = 2.5f;
    [SerializeField] Transform attackRaycastOrigin;
    public delegate void DinoAnim(int animIndex);  //delegate that allows animation to change 
    public static event DinoAnim dinoAnimInfo;
    private int walkIndex = 0;
    private int attackIndex = 1;
    private int runIndex = 2;
    private int idleIndex = 3;
    private int roarIndex = 4;
    private int jumpIndex = 5;
    private int jumpToAttackIndex = 6;
    private int activeIdleIndex = 7;
    public LayerMask layerMask;
    private bool canBite = false;
    private float lengthOfBite = 2.333f;
    [SerializeField] Transform jumpTarget;
    private Transform[] navMeshTargets = new Transform[2];
    public Animator _animator;
    public Transform attackPoint;
    public float attackRange = .5f;
    public LayerMask playerLayer;
    public int attackDamage = 10;
    private bool playerIsDead = false;
    private bool dinoIsDead = false;





    // Start is called before the first frame update
    void Start()
    {
        navMeshTargets[0] = _player;
        navMeshTargets[1] = jumpTarget;
        rb = this.GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        dinoAnimInfo(activeIdleIndex);
        playerLayer |= (1 << 6);
        playerLayer |= (1 << 3);
        //AnimationEvent ae = new AnimationEvent(); 
        //ae.messageOptions = SendMessageOptions.DontRequireReceiver;


        //override the NavMesh rotation so we can manually rotate the dino in Follow()
        _enemy.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (!playerIsDead && !dinoIsDead)
            Follow();
        if (Input.GetButton("Jump"))
        {
            Jump();
        }

    }

    void Follow()
    {
        Vector3 navMeshVelocity = _enemy.velocity;  //how fast that dino movin'?
        float distance = Vector3.Distance(transform.position, _player.position);  //calculate distance from dino to player
        if (distance <= _minFollowDistance) // && distance >= _attackDistance)
        {
            if (distance >= _aggroDistance)
            {
                // _enemy.velocity *= 2f;
                dinoAnimInfo(runIndex);
                //Debug.Log("run velocity: " + navMeshVelocity);
            }
            //  if (!canBite)
            _enemy.SetDestination(navMeshTargets[0].position);

            if (dinoIsDead)
                _enemy.isStopped = true;

            if (distance < _aggroDistance && distance >= _attackDistance)
            {
                // 
                //  _enemy.velocity /= 2f;
                // Debug.Log("walk velocity: " + navMeshVelocity);
                dinoAnimInfo(walkIndex);
                // _animator.Play("Roar2");
                // audioSource.PlayOneShot(audioClip, .75f);
                //audioSource.Play();
            }
            if (distance < _attackDistance)
            {
                _enemy.velocity = new Vector3(0, 0, 0);

                if (!canBite)
                    dinoAnimInfo(idleIndex);
                //   Debug.Log("attack distance velocity: " + navMeshVelocity);
                // _enemy.SetDestination(jumpTarget.position);
                if (canBite)
                {
                    dinoAnimInfo(jumpToAttackIndex);
                    // Attack();
                    // _animator.runtimeAnimatorController.
                    //  _animator.SetTrigger("Attack");
                    //dinoAnimInfo(attackIndex);                              
                    //dinoAnimInfo(jumpIndex);

                    //anim override
                    // dinoAnimInfo(jumpToAttackIndex);



                    Vector3 jumpVector = _player.position - transform.position;
                    //rb.MovePosition(transform.position + jumpVector * Time.deltaTime * moveSpeed * 50f);
                    //_enemy.SetDestination(jumpTarget.position);
                }
                // Debug.Log("coroutine has ended");
                // dinoAnimInfo(roarIndex);


                //  Ray ray = new Ray(attackRaycastOrigin.position, Vector3.down); // * 0.5f); // + Vector3.up * 0.5f, Vector3.down);

                //  if (Physics.Raycast(ray, out RaycastHit hit, 10f, layerMask))
                //  {
                //  Debug.Log("hit: " + hit.transform.tag);
                //  Debug.DrawRay(attackRaycastOrigin.position, hit.point, Color.green);
                //  if (hit.transform.CompareTag("Player"))
                //   {
                //       Debug.Log("Dino is looking at player");
                //  }
                // }
            }

            //  if (!_enemy.isStopped)  //rotate the dino
            // {
            var targetPosition = _player.position;
            var targetPoint = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
            var _direction = (targetPoint - transform.position).normalized;
            var _lookRotation = Quaternion.LookRotation(_direction);
            // rb.rotation = Quaternion.RotateTowards(transform.rotation, _lookRotation, 360);

            //use this one
            if (!dinoIsDead)
                rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, _lookRotation, 360));

            //  }

        }
    }


    void Jump()
    {
        //  rb.AddForce(0, 200f, 0, ForceMode.Impulse);
    }

    public void DinoCanBite(bool bite)
    {
        canBite = bite;
        // Debug.Log("DinoCanBite called\ncaBite: " + canBite);
    }

    public void PlayerIsDead(bool playerDead)
    {
        playerIsDead = playerDead;
    }

    public void DinoIsDead(bool dinoDead)
    {
        dinoIsDead = dinoDead;
    }

    private void OnEnable()
    {
        AttackTrigger.dinoFacingPlayerInfo += DinoCanBite;
        PlayerCombat.playerIsDeadInfo += PlayerIsDead;
        Dinosaur.dinoIsDeadInfo += DinoIsDead;
    }

    private void OnDisable()
    {
        AttackTrigger.dinoFacingPlayerInfo -= DinoCanBite;
        PlayerCombat.playerIsDeadInfo -= PlayerIsDead;
        Dinosaur.dinoIsDeadInfo -= DinoIsDead;
    }

    private void Growl()
    {
        // Vector3 jumpVector =  transform.position -_player.position;
        // rb.MovePosition(transform.position + jumpVector * Time.deltaTime * moveSpeed * 500f);
        //rb.position = transform.position + jumpVector * Time.deltaTime * moveSpeed * 500f;
        // rb.position = transform.position * Time.deltaTime * moveSpeed * 500f;
        //_enemy.SetDestination(jumpTarget.position);
        // Debug.Log("JumpGrowl");
    }
    public void DinoAttack()
    {
         Debug.Log("Dino Attack has been called ");
        // dinoAnimInfo(jumpToAttackIndex);
        Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

        foreach (Collider knight in hitPlayer)
        {
            if (knight.TryGetComponent(out IDamagable hit))
            {
                // Debug.Log(knight.name + " hit");
                hit.Damage();
            }
            // Debug.Log(knight.name + " hit");
            // if (knight != null)
            // knight.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
            // playerHealth
            //    .TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
