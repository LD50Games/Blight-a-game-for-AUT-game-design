using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    [Header("Combat")]
    public int health;
    public bool in_combat = false;
    public Transform MaceHead;
    bool attacking = false;
    public LayerMask enemies;
    public GameObject source;

    [Header("Ai control panel")]
    public Transform destination;
    public NavMeshAgent agent;
    public Rigidbody rb;
    
    [Header("Presentaiton asignments")]
    public Animator animator;


    [Header("External asigned objects")]

    public GameObject player;


    GameObject agent_target;
    
    
    void Start()
    {
        agent_target = player;
    }

    // Update is called once per frame
    void Update()
    {

        animator.SetFloat("Speed", agent.velocity.sqrMagnitude);

        if (Vector3.Distance(player.transform.position, transform.position) < 1.5)
        {
            rb.isKinematic = false;
            agent.enabled = false;
            
            if (!attacking)
            {
                StartCoroutine(attack(1.6f));
                animator.SetTrigger("SlashDown");
            }
        }
        else
        {
            rb.isKinematic = true;
            agent.enabled = true;


            agent.SetDestination(agent_target.transform.position);
        }

    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            die();
        }
    }
    public void CollisionCheck(Transform orgin_)
    {
        Collider[] hits = Physics.OverlapSphere(orgin_.position, 0.6f, enemies);

        foreach (Collider hit in hits)
        {
            hit.SendMessage("TakeDamage", 4);
        }
    }
    public void die()
    {
        Destroy(source);
    }
    IEnumerator attack(float seconds)
    {
        attacking = true;

        yield return new WaitForSeconds(seconds * 1f);
        CollisionCheck(MaceHead);
        yield return new WaitForSeconds(seconds * 0.2f);
        CollisionCheck(MaceHead);
        yield return new WaitForSeconds(seconds * 0.1f);
        CollisionCheck(MaceHead);
        yield return new WaitForSeconds(seconds * 0.1f);
        yield return new WaitForSeconds(seconds * 2f);
        attacking = false;
    }
}