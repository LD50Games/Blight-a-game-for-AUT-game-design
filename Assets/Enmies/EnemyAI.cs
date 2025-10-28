using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class EnemyAI : MonoBehaviour
{

    [Header("Combat")]
    public int health;
    public bool in_combat = false;
    public Transform MaceHead;
    bool attacking = false;
    bool inMotion = false;
    public LayerMask enemies;
    public GameObject source;
    public Collider collider_;
    [Header("Ai control panel")]
    public Transform destination;
    public NavMeshAgent agent;
    public Rigidbody rb;
    
    [Header("Presentaiton asignments")]
    public Animator animator;


    [Header("External asigned objects")]
    public GameObject blood;
    public GameObject player;
    public GameObject phlegm;

    GameObject agent_target;
    private Coroutine attackCoroutine;
    [Header("Audio")]
    public AudioSource speaker;
    public AudioClip slash;
    public AudioClip fall;
    void Start()
    {
        agent_target = player;
        phlegm.SendMessage("hide");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.sqrMagnitude);

        if (Vector3.Distance(player.transform.position, transform.position) < 1.7 && !attacking && health > 0)
        {
            rb.isKinematic = false;
            agent.enabled = false;

            bool attackRandomiser = Random.value > 0.5f;

            if (attackRandomiser)
            {
                
                animator.SetTrigger("SlashDown");

                attackCoroutine = StartCoroutine(attack(1.6f));
                
            }
            else 
            {

                animator.SetTrigger("MidSlash");

                attackCoroutine = StartCoroutine(attack(0.9f));
                
            }
        }
        
        else
        {
            if (health > 0)
            {
                rb.isKinematic = true;
                agent.enabled = true;


                agent.SetDestination(agent_target.transform.position);
            }
        }

    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            StartCoroutine(die());
            
        }
        StartCoroutine(hit());
    }
    public void CollisionCheck(Transform orgin_)
    {
        Collider[] hits = Physics.OverlapSphere(orgin_.position, 0.6f, enemies);

        foreach (Collider hit in hits)
        {
            hit.SendMessage("TakeDamage", 4);
            Instantiate(blood, hit.transform.position, transform.rotation);
        }
    }
    public IEnumerator die()
    {
        animator.SetTrigger("Dead");
        rb.isKinematic = true ;
        collider_.enabled = false;
        agent.enabled = false;
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            }
        this.enabled = false;
        yield return new WaitForSeconds(1f);
        speaker.clip = fall;
        speaker.Play();

    }
    public IEnumerator attack(float seconds)
    {
        attacking = true;
        yield return new WaitForSeconds(seconds * 0.7f);
        inMotion = true;
        rb.isKinematic = true;
        agent.enabled = true;
        yield return new WaitForSeconds(seconds *0.6f);
        speaker.clip = slash;
        speaker.Play();
        CollisionCheck(MaceHead);
        yield return new WaitForSeconds(seconds * 0.2f);
        CollisionCheck(MaceHead);
        yield return new WaitForSeconds(seconds * 0.1f);
        CollisionCheck(MaceHead);
        yield return new WaitForSeconds(seconds * 0.1f);
        yield return new WaitForSeconds(seconds * 2f);
        inMotion = false;
        attacking = false;
        rb.isKinematic = false;
        agent.enabled = false;
    }
    IEnumerator hit()
    {
        rb.isKinematic = false;
        if (attackCoroutine != null && inMotion == false )
        {
            StopCoroutine(attackCoroutine);
            animator.SetTrigger("hit");
            attackCoroutine = null;
        }
        attacking = true ;
        yield return new WaitForSeconds(3f);
        attacking = false ;

    }
}