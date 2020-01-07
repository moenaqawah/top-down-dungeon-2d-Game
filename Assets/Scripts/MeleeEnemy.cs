using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeEnemy : Enemy
{

    [SerializeField] float moveSpeed=1f;
    [SerializeField] float attackSpeed = 0.3f;
    [SerializeField] float attackDamage = 50f;
    [SerializeField] Slider healthComponent=null;
    [SerializeField] Canvas canvas=null;

     Transform player=null;
     Rigidbody2D rb=null;
     Slider healthUI = null;
    Coroutine attackCoroutine = null;

    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
        rb = GetComponent<Rigidbody2D>();

        healthUI = Instantiate(healthComponent, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity) as Slider;
        healthUI.transform.parent = canvas.transform;

    }

    private void Update()
    {
       base.healthBar(healthUI);
    }

    

    protected override IEnumerator attack(GameObject player)
    {
        while (true)
        {
            if (player)
            {
                player.GetComponent<SpriteRenderer>().color = Color.red;
                player.GetComponent<Health>().decreaseCurrentHealth(attackDamage);
                moveSpeed = 0;
                yield return new WaitForSeconds(attackSpeed);
                player.GetComponent<SpriteRenderer>().color = Color.white;
                yield return new WaitForSeconds(attackSpeed);
            }
        }
    }

    private void OnDestroy()
    {
        if (healthUI.gameObject)
        {
            Destroy(healthUI.gameObject);
        }
    }

    protected override void move()
    {
        if (player)
        {
            Vector2 direction = player.position - transform.position;
            lookatPlayer(direction);
            direction.Normalize();
            rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            attackCoroutine = StartCoroutine(attack(collision.gameObject));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            stopAttacking(collision.gameObject);
        }
    }

    private void stopAttacking(GameObject player)
    {
        if (player)
        {
            moveSpeed = 1.5f;
            StopCoroutine(attackCoroutine);
            player.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

   
}
