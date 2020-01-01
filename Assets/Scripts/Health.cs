using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] float minHealth = 0;
    [SerializeField] float maxHealth = 100;
    [SerializeField] float currentHealth = 100;

    public void decreaseCurrentHealth(float value)
    {
        currentHealth-=value;
        if (currentHealth <= minHealth)
        {
            Destroy(gameObject);
        }
    }

    public float getCurrentHealthPercentage()
    {
        return (currentHealth-minHealth) / (maxHealth - minHealth);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
