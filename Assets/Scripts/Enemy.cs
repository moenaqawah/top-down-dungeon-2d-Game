using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
   

    protected abstract void move();

    protected abstract IEnumerator attack(GameObject player);



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        move();
    }

    protected virtual void lookatPlayer(Vector2 direction)
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

    protected void healthBar(Slider healthUI)
    {
        Vector2 healthUiPosition = new Vector2(transform.position.x, transform.position.y + 0.5f);
        healthUI.transform.position = Camera.main.WorldToScreenPoint(healthUiPosition);
        healthUI.value = GetComponent<Health>().getCurrentHealthPercentage();
    }


}
