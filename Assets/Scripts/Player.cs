﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{

    [SerializeField] float attackRange = 5f;
    [SerializeField] GameObject bullet = null;
    [SerializeField] float timeBetweenBullets = 0.5f;
    [SerializeField] Slider healthComponent = null;
    [SerializeField] Canvas canvas = null;


    Transform closestEnemy = null;
    bool isFiring = false;
    Coroutine fireCoroutine = null;
    Slider healthUI = null;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        healthUI = Instantiate(healthComponent, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity) as Slider;
        healthUI.transform.parent = canvas.transform;
    }

    private void healthBar()
    {
        Vector2 healthUiPosition = new Vector2(transform.position.x, transform.position.y + 0.5f);
        healthUI.transform.position = Camera.main.WorldToScreenPoint(healthUiPosition);
        healthUI.value = GetComponent<Health>().getCurrentHealthPercentage();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (FindClosestEnemy())
        {
            if (!isFiring)
            {
                fireCoroutine=StartCoroutine(Fire());
                isFiring = true;
            }
        }
        else
        {
            if (isFiring)
            {
                StopCoroutine(fireCoroutine);
                isFiring = false;
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

    private void Update()
    {
        healthBar();
    }

    IEnumerator Fire()
    {

        while (true)
        {
           Instantiate(bullet, transform.position, Quaternion.identity);
           yield return new WaitForSeconds(timeBetweenBullets);
        }
    }


    public Transform getClosestEnemy()
    {
        return closestEnemy;
    }

    private bool FindClosestEnemy()
    {
        LayerMask enemyLayer = LayerMask.GetMask("Enemy");
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        if (collider2Ds.Length > 0)
        {
            float minDistance = Vector2.Distance(transform.position, collider2Ds[0].transform.position);
            closestEnemy = collider2Ds[0].transform;
            foreach (Collider2D collider in collider2Ds)
            {
                if (minDistance > Vector2.Distance(transform.position, collider.transform.position))
                {
                    closestEnemy = collider.transform;
                }
            }
            return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
     
     Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
