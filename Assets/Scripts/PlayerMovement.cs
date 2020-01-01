using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    [SerializeField] Joystick joystick=null;
    [SerializeField] float moveSpeed = 20f;


    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        rb.velocity = new Vector3(joystick.Horizontal * moveSpeed * Time.deltaTime, joystick.Vertical * moveSpeed * Time.deltaTime);
    }
}
