using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAiScript : MonoBehaviour
{
    public float zombieDamage;
    public float zombieLife;
    public float destinationUpdateTime;
    public float foundPlayerRange;
    public float attackRange;
    public float attackCurrency;
    public float mapWidth;
    public float mapHeight;
    public GameObject zombieHitParticles;
    public GameObject zombieDeadParticles;

    private NavMeshAgent navMeshZombie;
    private Animator zombieAnim;
    private Rigidbody rgZombie;
    private GameObject player;
    private float time;
    private float attackingTimer;
    private bool nearPlayer;
    private bool dead;
    private float distance;

    void Start()
    {
        navMeshZombie = GetComponent<NavMeshAgent>();
        zombieAnim = GetComponent<Animator>();
        rgZombie = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= destinationUpdateTime && !nearPlayer)
        {
            SetDestination();
        }

        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < foundPlayerRange)
        {
            nearPlayer = true;
            navMeshZombie.destination = player.transform.position;
            if (distance < attackRange)
            {
                attackingTimer += Time.deltaTime;
                if (attackingTimer > attackCurrency && !dead)
                {
                    attackingTimer = 0;
                    zombieAnim.SetTrigger("Punch");
                    player.GetComponent<PlayerController>().playerGetHit(zombieDamage);
                }
            }
        }
        else
        {
            nearPlayer = false;
        }
    }

    private void SetDestination()
    {
        float randomX = Random.Range(-mapWidth, mapWidth);
        float randomZ = Random.Range(-mapHeight, mapHeight);
        navMeshZombie.destination = new Vector3(randomX, 0, randomZ);
        time = 0;
    }

    public void ZombieHitted(float damage)
    {
        if (!dead)
        {
            zombieLife -= damage;
            if (zombieLife < 0)
            {
                dead = true;
                rgZombie.isKinematic = true;
                zombieAnim.SetTrigger("Dead");
                var particles = Instantiate(zombieDeadParticles, transform.position + Vector3.up, Quaternion.Euler(0, 0, 0), transform);
                Destroy(particles, 1.5f);
                player.GetComponent<ExtraItemsScript>().DropRandomItem(transform.position);
                Destroy(gameObject, 3f);
            }
            else
            {
                zombieAnim.SetTrigger("Hitted");
                var particles = Instantiate(zombieHitParticles, transform.position + Vector3.up, Quaternion.Euler(0, 0, 0), transform);
                Destroy(particles, 3f);
            }
        }
    }
}
