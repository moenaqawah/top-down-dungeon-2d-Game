using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float bulletDamage = 10f;

    Transform closestEnemy = null;
    Vector3 target = new Vector2();
    Rigidbody2D rb = null;

   

    private void Start()
    {
        closestEnemy = FindObjectOfType<Player>().getClosestEnemy();
        target = closestEnemy.position;
        rb = GetComponent<Rigidbody2D>();
        moveBullet();

    }

    private void moveBullet()
    {
        Vector2 distance = (Vector2)(target - transform.position);
        distance.Normalize();
        rb.velocity = distance * bulletSpeed * Time.deltaTime;
    }

    private void FixedUpdate()
    {

        if (closestEnemy == null)
        {
            Destroy(gameObject);
            return;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bulletHitsAnObject(collision.gameObject);
        
    }

    private void bulletHitsAnObject(GameObject enemy)
    {
        Health health = enemy.GetComponent<Health>();
        if (health)
        {
            health.decreaseCurrentHealth(bulletDamage);
            
        }
        Destroy(gameObject);
    }

}
