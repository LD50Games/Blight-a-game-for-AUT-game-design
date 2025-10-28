using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

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
    public List<GameObject> hidepositions = new List<GameObject>();
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
            agent_target = player;
        }
        else { 
            rb.isKinematic = true;
            agent.enabled = true;
            

            agent.SetDestination(agent_target.transform.position);
            if (Vector3.Distance(player.transform.position, transform.position) > 20)
            {
                agent_target = player;
            }
        }
        
    }
    public void hide()
    {
        float distance_ = 10000000;
        foreach (GameObject t in hidepositions)
        {
            float distance = Vector3.Distance(t.transform.position, transform.position);
            if (distance < distance_)
            {
                distance_ = distance;
                agent_target = t;
                
            }
            print(distance_);
        }
    }
    public void Follow()
    {
        agent_target = player;
    }
}
