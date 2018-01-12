using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : MonoBehaviour
{ 
    public float Damage = 10.0f;

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            var hit = col.gameObject;
            var health = hit.GetComponent<Enemy_Health>();

            health.takeDamage(Damage);

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }
}
