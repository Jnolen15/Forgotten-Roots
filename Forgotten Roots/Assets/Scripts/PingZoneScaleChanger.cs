using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingZoneScaleChanger : MonoBehaviour
{
    [SerializeField] float startingScale = .1f;
    [SerializeField] float finalScale = .7f;
    [SerializeField] float lerpVal = .4f;

    float currentScale;
    bool finishedScaling = false;

    // Start is called before the first frame update
    void Start()
    {
        currentScale = startingScale;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentScale == finalScale)
        {
            print("Done scaling");
            finishedScaling = true;
        }

        if (!finishedScaling)
        {
            print("scaling");
            currentScale = Mathf.Lerp(currentScale, finalScale, lerpVal);
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);
        }
    }
}
