using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{

    private Rigidbody2D rb;

    private bool launched = false;

    public bool catchable = false;

    public string notename;

    void Awake()
    {
        transform.Rotate(0.0f, 0.0f, Random.Range(-15.0f, 15.0f));
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(catchCooldown());
    }

    void Update()
    {
        if(!launched)
        {
            rb.AddForce(transform.up * 10, ForceMode2D.Impulse);
            launched = true;
        } 
    }

    IEnumerator catchCooldown()
    {
        yield return new WaitForSeconds(0.4f);
        catchable = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Surface")
        {
            rb.velocity = new Vector2(0, 0);
            rb.gravityScale = 0;
        }
    }

}
