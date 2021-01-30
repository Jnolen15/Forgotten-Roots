using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{

    private Rigidbody2D rb;

    private bool launched = false;

    public bool catchable = false;

    void Awake()
    {
        transform.Rotate(0.0f, 0.0f, Random.Range(-25.0f, 25.0f));
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

}
