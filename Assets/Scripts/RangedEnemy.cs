using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangedEnemy : Enemy
{

    [SerializeField] float attackSpeed = 2f;
    [SerializeField] float attackDamage = 50f;
    [SerializeField] Slider healthComponent = null;
    [SerializeField] Canvas canvas = null;
    [SerializeField] GameObject weapon = null;

    Transform playerPos = null;
    Rigidbody2D rb = null;
    Slider healthUI = null;
    Coroutine attackCoroutine = null;

    protected override IEnumerator attack(GameObject player)
    {
        Instantiate(weapon, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(attackSpeed);
    }

    protected override void move()
    {
        Vector2 direction = playerPos.position - transform.position;
        lookatPlayer(direction);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerPos = FindObjectOfType<Player>().transform;

        healthUI = Instantiate(healthComponent, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity) as Slider;
        healthUI.transform.parent = canvas.transform;
    }

    

    // Update is called once per frame
    void Update()
    {
        base.healthBar(healthUI);
    }

    private void OnDestroy()
    {
        if (healthUI.gameObject)
        {
            Destroy(healthUI.gameObject);
        }
    }

    protected override void lookatPlayer(Vector2 direction)
    {

        if (direction.x <= 0)
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.y = 180;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
        else
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.y = 0;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
    }


}
