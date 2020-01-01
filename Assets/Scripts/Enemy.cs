using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
   

    protected abstract void move();

    protected abstract void attack();



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        move();
    }



}
