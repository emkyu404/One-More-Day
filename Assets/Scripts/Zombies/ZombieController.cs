using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody))]
public class ZombieController : MonoBehaviour
{
    NavMeshAgent agent;
    private Vector3 target;
    [SerializeField] Transform PlayerTransform;

    [SerializeField] private float health = 100;


    [SerializeField] private int damage = 25;
    [SerializeField] private float minDistAttack = 0.5f;
    [SerializeField] private float AttackDelay = 1f;

    [SerializeField] HealthSystem playerHealth;

    Vector3 knockBackDirection;
    float LastAttackTime;
    bool knockBack;
    private CapsuleCollider collider;
    [SerializeField] Animator animator;

    private bool isActive;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        collider = GetComponent<CapsuleCollider>();

        GameObject player = GameObject.Find("Player");
        PlayerTransform = player.transform;
        playerHealth = player.GetComponent<HealthSystem>();
        isActive = true;
    }

    private void Update()
    {
        if (isActive)
        {
            float dist = Vector3.Distance(transform.position, target);

            if (dist <= minDistAttack)
            {
                AttackPlayer();
            }
            else
            {
                ChasePlayer();
            }
        }
    }

    private void FixedUpdate()
    {
        if (knockBack)
        {
            agent.velocity = knockBackDirection;
        }
    }


    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        if (LastAttackTime + AttackDelay < Time.time)
        {
            playerHealth.TakeDamage(damage);
            animator.SetTrigger("Attack");
            LastAttackTime = Time.time;
        }
    }
    private void ChasePlayer()
    {
        agent.SetDestination(target);
        transform.LookAt(new Vector3 (PlayerTransform.position.x, transform.position.y, PlayerTransform.position.z));
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Bullet"))
        {
            float amount = other.gameObject.GetComponent<Bullet>().GetDamage();
            TakeDamage(amount);
        }
    }

    private void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            isActive = false;
            int randomNumber = Random.Range(1, 3);
            animator.SetTrigger("Death"+ randomNumber);
            agent.SetDestination(transform.position); // immobilize
            collider.enabled = false;
            Destroy(gameObject, 10f);
            return;
        }
        StartCoroutine(Knockback());

    }

    IEnumerator Knockback()
    {
        knockBack = true;
        agent.speed = 20;
        agent.angularSpeed = 0;
        agent.acceleration = 20;

        yield return new WaitForSeconds(0.1f);

        knockBack = false;
        agent.speed = 7;
        agent.angularSpeed = 180;
        agent.acceleration = 7;
    }

    public void SetTarget(Vector3 position)
    {
        target = position;
    }


}
