using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillLineDrawer : MonoBehaviour
{
    [SerializeField] float lineWidth = .5f;

    public GameObject linePrefab;
    public GameObject currentLine;

    public LineRenderer lineRenderer;
    public List<Vector2> fingerPositions;

    public Transform targetToFollow;

    private bool drawing = false;

    // Start is called before the first frame update
    void Start()
    {
        targetToFollow = GetComponentInParent<Transform>();
        StartDrawing();
    }

    // Update is called once per frame
    void Update()
    {
        if (drawing)
        {
            Vector2 tempFingerPos = targetToFollow.position;
            {
                if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > .1f) // line distance variable should be adjustable
                {
                    UpdateLine(tempFingerPos);
                }
            }
        }
    }

    void CreateLine()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        lineRenderer.widthMultiplier = lineWidth;
        fingerPositions.Clear();
        fingerPositions.Add(targetToFollow.position);
        fingerPositions.Add(targetToFollow.position);
        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);
    }

    void UpdateLine(Vector2 newFingerPos)
    {
        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
    }

    public void StartDrawing()
    {
        CreateLine();
        drawing = true;
    }

    public void StopDrawing()
    {
        drawing = false;
    }
}

// Created with https://www.youtube.com/watch?v=pa_U64G7gkE