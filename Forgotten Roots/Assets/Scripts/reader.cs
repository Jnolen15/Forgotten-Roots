using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reader : MonoBehaviour
{

    public string current;
    public Controller cont;
    public GameObject currentPage;

    private bool pageOpen = false;
    private bool done = false;

    // Update is called once per frame
    void Update()
    {
        current = cont.currentNote;

        if (cont.reading == true)
        {
            if(pageOpen == false)
            {
                // Open First Page
                currentPage = GameObject.Find(current);
                currentPage.GetComponent<SpriteRenderer>().enabled = true;
                pageOpen = true;
            }
            

            // Next Page
            if (currentPage.transform.childCount > 0)
            {
                if (Input.GetButtonDown("Activate"))
                {
                    currentPage.GetComponent<SpriteRenderer>().enabled = false;
                    currentPage = currentPage.transform.GetChild(0).gameObject;
                    currentPage.GetComponent<SpriteRenderer>().enabled = true;
                }

            } else
            {
                done = true;
            }

            // Close Note
            if (Input.GetButtonDown("Activate") && done)
            {
                cont.reading = false;
                currentPage.GetComponent<SpriteRenderer>().enabled = false;
                pageOpen = false;
                cont.justFinished = true;
            }
        }
    }
}
