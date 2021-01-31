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
    private bool reading = false;
    private bool readingTitle = true;
    private bool readingTutorial = false;

    Rigidbody2D rb;
    Vector2 movement;
    public GameObject clawAim;
    public GameObject aimArrow;
    public GameObject drillHead;
    public GameObject launchZone;
    public string currentNote;
    public GameObject currentPage;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (readingTitle)
        {
            readingTitle = false;
        }

        // CORE LOOP
        if (!readingTitle)
        {
            if (!reading)
            {
                if (!drillOut)
                {
                    if (!isDrilling) // Player movement
                    {
                        movement.x = Input.GetAxisRaw("Horizontal");
                        animator.SetFloat("Speed", Mathf.Abs(movement.x));
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
            else
            {
                // Open Page
                currentPage = GameObject.Find(currentNote);
                currentPage.GetComponent<SpriteRenderer>().enabled = true;

                // Close Page
                if (Input.GetButtonDown("Activate"))
                {
                    reading = false;
                    currentPage.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
        
    }

    void FixedUpdate()
    {
        if (!isDrilling && !reading)
        {
            rb.MovePosition(rb.position + movement * speed);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }

    void drillMode()
    {
        GameObject drill = Instantiate(drillHead, aimArrow.transform.position, aimArrow.transform.rotation);
        drillOut = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Drill")
        {
            isDone = true;
            if (col.gameObject.GetComponent<drillControll>().heldItem == null)
            {
                Destroy(col.gameObject);
            }
            else if (col.gameObject.GetComponent<drillControll>().heldItem.tag == "Rock")
            {
                Instantiate(col.gameObject.GetComponent<drillControll>().heldItem.GetComponent<Rock>().throwable, launchZone.transform.position, transform.rotation);
                Destroy(col.gameObject.GetComponent<drillControll>().heldItem);
                Destroy(col.gameObject);
            }
            else if (col.gameObject.GetComponent<drillControll>().heldItem.tag == "Artifact")
            {
                Instantiate(col.gameObject.GetComponent<drillControll>().heldItem.GetComponent<Artifact>().throwable, launchZone.transform.position, transform.rotation);
                Destroy(col.gameObject.GetComponent<drillControll>().heldItem);
                Destroy(col.gameObject);
            }
        }

        if (col.gameObject.tag == "ThrowableRock")
        {
            if (col.gameObject.GetComponent<Launch>().catchable)
            {
                Destroy(col.gameObject);
            }
        }

        if (col.gameObject.tag == "ThrowableArtifact")
        {
            if (col.gameObject.GetComponent<Launch>().catchable)
            {
                reading = true;
                currentNote = col.gameObject.GetComponent<Launch>().notename;
                Destroy(col.gameObject);
            }
        }
    }
}
