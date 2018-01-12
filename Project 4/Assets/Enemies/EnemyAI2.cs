using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Behaviors2 { Guard, Attack };

public class EnemyAI2 : MonoBehaviour
{

    public Behaviors2 aiBehaviors = Behaviors2.Guard;

    public bool inSightRange = false;
    public bool inHitRange = false;

    //for enemy and player interation
    public Transform player;
    float PlayerDistance;
    private float hitTime = 1.00f;


    //for traversing waypoints
    UnityEngine.AI.NavMeshAgent navAgent;
    Vector3 Destination;
    float Distance;
    public Transform[] Waypoints;
    public int curWaypoint = 0;
    bool ReversePath = false;

    void Start()
    {
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    void Update()
    {
        hitTime -= Time.deltaTime;
        if (hitTime < 0)
        {
            HitTarget();
            hitTime = 1.00f;
        }
        RunBehaviors();
    }

    void RunBehaviors()
    {
        switch (aiBehaviors)
        {
            case Behaviors2.Guard:
                Guard();
                break;
            case Behaviors2.Attack:
                Attack();
                break;
        }
    }

    void ChangeBehavior(Behaviors2 newBehavior)
    {
        aiBehaviors = newBehavior;

        RunBehaviors();
    }

    void Guard()
    {
        GetComponent<Animation>().Play("run");
        SearchForTarget();
        if (inSightRange)
        {
            Destination = GameObject.FindGameObjectWithTag("Character").transform.position;
            navAgent.SetDestination(Destination);
            if (inHitRange)
            {
                ChangeBehavior(Behaviors2.Attack);
            }
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        //for waypoint traversal
        Distance = Vector3.Distance(gameObject.transform.position,
            Waypoints[curWaypoint].position);

        //for moving around waypoints
        if (Distance > 3.00f)
        {
            Destination = Waypoints[curWaypoint].position;
            navAgent.SetDestination(Destination);
        }
        else
        {
            if (ReversePath)
            {
                if (curWaypoint <= 0)
                {
                    ReversePath = false;
                }
                else
                {
                    curWaypoint--;
                    Destination = Waypoints[curWaypoint].position;
                }
            }
            else
            {
                if (curWaypoint >= Waypoints.Length - 1)
                {
                    ReversePath = true;
                }
                else
                {
                    curWaypoint++;
                    Destination = Waypoints[curWaypoint].position;
                }
            }
        }
    }

    void SearchForTarget()
    {
        PlayerDistance = Vector3.Distance(transform.position, player.position);
        if (PlayerDistance < 8.00f)
        {
            inSightRange = true;
            if (PlayerDistance < 2.60f)
            {
                inHitRange = true;
            }
            else
            {
                inHitRange = false;
            }
        }
        else
        {
            inSightRange = false;
        }

    }

    void Attack()
    {
        GetComponent<Animation>().Play("attack");
        SearchForTarget();
        if (inHitRange == true)
        {
            Destination = GameObject.FindGameObjectWithTag("Character").transform.position;
            transform.LookAt(Destination);
        }
        else
        {
            ChangeBehavior(Behaviors2.Guard);
        }
    }

    void HitTarget()
    {
        if (inHitRange == true)
        {
            var health = GameObject.FindGameObjectWithTag("Character").GetComponent<Inventory>();
            health.takeDamage(10.0f);
        }
    }
}
