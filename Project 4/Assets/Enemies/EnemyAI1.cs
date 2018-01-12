using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Behaviors1 {Idle, Shoot};

public class EnemyAI1 : MonoBehaviour
{
    public Behaviors1 aiBehaviors = Behaviors1.Idle;

    private bool inSightRange = false;
    private bool inshootRange = false;

    UnityEngine.AI.NavMeshAgent navAgent;
    Vector3 Destination;
    float Distance;

    //for enemy firing
    private float shootTime = 1.00f;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    // Use this for initialization
    void Start ()
    {
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Destination = GameObject.FindGameObjectWithTag("Character").transform.position;
        Distance = Vector3.Distance(gameObject.transform.position, Destination);
        shootTime -= Time.deltaTime;
        if (shootTime < 0)
        {
            FireBullet();
            shootTime = 1.00f;
        }
        RunBehaviors();  
	}

    void RunBehaviors()
    {
        switch(aiBehaviors)
        {
            case Behaviors1.Idle:
                Idle();
                break;
            case Behaviors1.Shoot:
                ShootAtTarget();
                break;
        }
    }

    void ChangeBehavior(Behaviors1 newBehavior)
    {
        aiBehaviors = newBehavior;

        RunBehaviors();
    }

    void Idle()
    {
        SearchForTarget();
        if (inSightRange)
        {
            navAgent.isStopped = false;
            GetComponent<Animation>().Play("run");
            navAgent.SetDestination(Destination);
            if (inshootRange)
            {
                ChangeBehavior(Behaviors1.Shoot);
            }
        }
        else
        {
            GetComponent<Animation>().Play("waitingforbattle");
            navAgent.isStopped = true;
        }
    }

    void SearchForTarget()
    {
        if(Distance < 10.00f)
        {
            inSightRange = true;
            if(Distance < 4.00f)
            {
                inshootRange = true;
            }
            else
            {
                inshootRange = false;
            }

        }
        else
        {
            inSightRange = false;
        }
    }

    void ShootAtTarget()
    {
        SearchForTarget();
        if(inshootRange)
        {
            transform.LookAt(Destination);
            GetComponent<Animation>().Play("waitingforbattle");
        }
        else
        {
            ChangeBehavior(Behaviors1.Idle);
        }
        
    }

    void FireBullet()
    {
        if(inshootRange == true)
        {
            // Create the Bullet from the Bullet Prefab
            var bullet = (GameObject)Instantiate(
                bulletPrefab,
                bulletSpawn.position,
                bulletSpawn.rotation);

            // Add velocity to the bullet
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

            // Destroy the bullet after 2 seconds
            Destroy(bullet, 1.5f);
        }
    }
}
