using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class phlegm : MonoBehaviour
{
    [Header("Ai control panel")]
    public Transform destination;
    public NavMeshAgent agent;
    public Rigidbody rb;

    [Header("Presentaiton asignments")]
    public Animator animator;


    [Header("External asigned objects")]
    
    public GameObject player;
    
    
    GameObject agent_target;
    public bool in_combat = false;
    public int health;
    [Header("Hide Positions")]
    public List<Transform> hidepositions = new List<Transform>();
    void Start()
    {
        agent_target = player;
    }

    // Update is called once per frame
    void Update()
    {

        animator.SetFloat("walking", agent.velocity.sqrMagnitude);

        if (Vector3.Distance(player.transform.position, transform.position) < 2)
        {
            rb.isKinematic = false;
            agent.enabled = false;
        }
        else { 
            rb.isKinematic = true;
            agent.enabled = true;
            

            agent.SetDestination(agent_target.transform.position);
        }
        
    }
    
}
