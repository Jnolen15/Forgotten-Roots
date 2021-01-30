using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private bool isGrabbed = false;

    private GameObject drill;

    public GameObject throwable;

    // Update is called once per frame
    void Update()
    {
        if (isGrabbed)
        {
            transform.position = drill.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Drill")
        {
            isGrabbed = true;

            drill = col.gameObject;
        }
    }
}
