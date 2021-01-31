using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drillControll : MonoBehaviour
{
    public float speed = 2f;
    public GameObject PingZonePrefab;

    private bool moving = true;
    private bool drillHit = false;
    private bool drillReturn = false;

    Rigidbody2D rb;
    DrillLineDrawer lineDrawer;
    CameraFollower cameraFollower;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lineDrawer = GetComponent<DrillLineDrawer>();
        cameraFollower = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollower>();
        cameraFollower.SetCameraTarget(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Activate") && drillHit && !drillReturn)
        {
            StartCoroutine(DrillMove());
        }
    }

    IEnumerator DrillMove()
    {
        drillReturn = true;
        rb.velocity = transform.up * speed;
        yield return new WaitForSeconds(0.5f);
        rb.velocity = new Vector2(0, 0);
        drillReturn = false;
    }

    void FixedUpdate()
    {
        if (moving)
        {
            rb.velocity = transform.up * -speed;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Rock")
        {
            lineDrawer.StopDrawing();
            rb.velocity = new Vector2(0, 0);
            moving = false;
            drillHit = true;
            Instantiate(PingZonePrefab, transform.position, Quaternion.identity);
        }

        if (col.gameObject.tag == "Boulder")
        {
            lineDrawer.StopDrawing();
            rb.velocity = new Vector2(0, 0);
            moving = false;
            rb.velocity = transform.up * speed;
            Instantiate(PingZonePrefab, transform.position, Quaternion.identity);
        }

        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
    void OnDestroy()
    {
        cameraFollower.ResetCameraTarget();
    }
}
