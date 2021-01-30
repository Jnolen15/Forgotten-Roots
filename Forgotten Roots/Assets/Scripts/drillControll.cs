using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drillControll : MonoBehaviour
{
    public float speed = 1f;

    Rigidbody2D rb;

    DrillLineDrawer lineDrawer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lineDrawer = GetComponent<DrillLineDrawer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            lineDrawer.StopDrawing();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = transform.up * -speed;
    }
}
