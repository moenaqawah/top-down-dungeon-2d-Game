using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float bulletDamage = 10f;

    Transform closestEnemy = null;
    Rigidbody2D rb = null;

    private void Start()
    {
        closestEnemy = FindObjectOfType<Player>().getClosestEnemy();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {

        if (closestEnemy == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector2 distance = (Vector2)(closestEnemy.position - transform.position);
        distance.Normalize();
        rb.velocity = distance * bulletSpeed * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       Health health=collision.GetComponent<Health>();
        if (health)
        {
            health.decreaseCurrentHealth(bulletDamage);
            Destroy(gameObject);
        }
        
    }

}
