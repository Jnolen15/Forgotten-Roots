using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 0.1f;
    public float aimSpeed = 100f;
    public float rightBound = 70f;

    private bool isDrilling = false;
    private bool isDone = false;

    Rigidbody2D rb;
    Vector2 movement;
    public GameObject clawAim;
    public GameObject aimArrow;
    public GameObject drillHead;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (!isDrilling)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
        }

        if (isDrilling)
        {
            if(Input.GetButtonDown("Activate"))
            {
                drillMode();
                Debug.Log("Fire!");
                isDone = true;
            }
            else
            {
                clawAim.transform.rotation = Quaternion.Euler(Vector3.forward * (Mathf.PingPong(Time.time * aimSpeed, rightBound) - 35));
            }
        }

        if (Input.GetButtonDown("Activate") && !isDrilling && !isDone)
        {
            isDrilling = true;
        }
        else if (isDone)
        {
            isDrilling = false;
            isDone = false;
        }
    }

    void FixedUpdate()
    {
        if (!isDrilling)
        {
            rb.MovePosition(rb.position + movement * speed);
        }
    }

    void drillMode()
    {
        Instantiate(drillHead, aimArrow.transform.position, aimArrow.transform.rotation, transform);
    }
}
