using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeEnemy : Enemy
{

    [SerializeField] float moveSpeed=1f;
    [SerializeField] Slider healthComponent=null;
    [SerializeField] Canvas Canvas=null;

     Transform player=null;
     Rigidbody2D rb=null;
    Slider healthUI = null;

    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
        rb = GetComponent<Rigidbody2D>();

        healthUI = Instantiate(healthComponent, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity) as Slider;
        healthUI.transform.parent = Canvas.transform;

    }

    private void Update()
    {
        healthBar();
    }

    private void healthBar()
    {
        Vector2 healthUiPosition = new Vector2(transform.position.x, transform.position.y + 0.5f);
        healthUI.transform.position = Camera.main.WorldToScreenPoint(healthUiPosition);
        healthUI.value = GetComponent<Health>().getCurrentHealthPercentage();
    }

    protected override void attack()
    {
        throw new System.NotImplementedException();
    }

    private void OnDestroy()
    {
        Destroy(healthUI.gameObject);
    }

    protected override void move()
    {
        Vector2 direction = player.position - transform.position;
        lookatPlayer(direction);
        direction.Normalize();
        rb.MovePosition((Vector2)transform.position+(direction*moveSpeed*Time.deltaTime));
    }


   

    private void lookatPlayer(Vector2 direction)
    {
        
        if (direction.x <= 0)
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.y = 0;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
        else
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.y = 180;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
    }
}
