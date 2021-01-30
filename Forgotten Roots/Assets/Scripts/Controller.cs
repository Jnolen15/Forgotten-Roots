using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 0.1f;
    public float aimSpeed = 100f;
    public float rightBound = 70f;

    private bool isDrilling = false;
    private bool drillOut = false;
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

        if (!drillOut)
        {
            if (!isDrilling) // Player movement
            {
                movement.x = Input.GetAxisRaw("Horizontal");
            }

            if (isDrilling) // Drill Aim
            {
                if (Input.GetButtonDown("Activate"))
                {
                    drillMode();
                }
                else
                {
                    clawAim.transform.rotation = Quaternion.Euler(Vector3.forward * (Mathf.PingPong(Time.time * aimSpeed, rightBound) - 35));
                }
            }
        }

        if (Input.GetButtonDown("Activate") && !isDrilling && !isDone) // Enter drill aim mode
        {
            isDrilling = true;
        }
        else if (isDone) // Go back to movement when done
        {
            isDrilling = false;
            isDone = false;
            drillOut = false;
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
        Instantiate(drillHead, aimArrow.transform.position, aimArrow.transform.rotation);
        drillOut = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Drill")
        {
            isDone = true;
        }

        if (col.gameObject.tag == "Rock")
        {
            Instantiate(col.gameObject.GetComponent<Rock>().throwable, transform.position, transform.rotation);
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "ThrowableRock")
        {
            if (col.gameObject.GetComponent<Launch>().catchable)
            {
                Destroy(col.gameObject);
            }
        }
    }
}
