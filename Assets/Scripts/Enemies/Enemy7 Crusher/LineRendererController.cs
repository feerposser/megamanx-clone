using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererController : MonoBehaviour
{
    LineRenderer lr;

    [SerializeField] Transform[] points;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lr.SetPosition(0, points[0].position);
        lr.SetPosition(1, points[1].position);   
    }
}
