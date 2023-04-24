using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseMonster : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;
    private float remainingDistance;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Target").transform;
        agent.destination = target.position;
    }

    void Update()
    {
        remainingDistance = Vector3.Distance(transform.position, target.position);
        if (remainingDistance < 5f)
        {
            GameManager.DamagePlayer();
            Destroy(gameObject);

        }
    }

}
