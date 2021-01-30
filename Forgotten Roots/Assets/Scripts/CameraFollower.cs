using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform defaultTarget;

    Transform targetTransform;

    public float lerpVal = .2f;
    public float maxSpeed = 1f;
    public float minDist = 0f;
    public float maxDist = 3f;

    float currSpeed;

    // Start is called before the first frame update
    void Start()
    {
        targetTransform = defaultTarget;
    }

    // Update is called once per frame
    void Update()
    {
        LerpFollowTarget();
    }

    void LerpFollowTarget()
    {
        currSpeed = maxSpeed * Mathf.InverseLerp(minDist, maxDist, Mathf.Abs(transform.position.y - targetTransform.position.y));

        float yMove = targetTransform.position.y - this.transform.position.y;
        Vector2 movement = new Vector2(0, yMove);

        transform.Translate(movement * currSpeed * Time.deltaTime);
    }

    public void SetCameraTarget(Transform target)
    {
        targetTransform = target;
    }

    public void ResetCameraTarget()
    {
        targetTransform = defaultTarget;
    }
}
