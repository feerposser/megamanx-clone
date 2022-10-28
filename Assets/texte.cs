using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class texte : MonoBehaviour
{

    PolygonCollider2D p;
    List<Vector2> points = new List<Vector2>();

    Vector3 detection;
    Vector3 detection2;

    void Start()
    {
        p = GetComponent<PolygonCollider2D>();

        SetPolygon();
    }

    Vector2 GetPoint(float x, float y)
    {
        return new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger");
        Debug.Log(collision.bounds);
        detection = collision.bounds.center;
        detection2 = collision.bounds.extents;

        Debug.Log(collision.bounds.center);
        Debug.Log(collision.bounds.extents);
        Debug.Log(collision.bounds.size);

        Vector3 left = collision.bounds.center + new Vector3(-collision.bounds.extents.x, 0, 0);
        Vector3 rigth = collision.bounds.center + new Vector3(collision.bounds.extents.x, 0, 0);

        detection = left;
        detection2 = rigth;
    }

    void SetPolygon()
    {
        points.Add(GetPoint(-.5f, .5f));

        points.Add(GetPoint(0, .5f));
        points.Add(GetPoint(0, -.2f));

        points.Add(GetPoint(0.2f, -.2f));
        points.Add(GetPoint(0.2f, .5f));


        points.Add(GetPoint(.5f, .5f));
        points.Add(GetPoint(.5f, -.5f));
        points.Add(GetPoint(-.5f, -.5f));

        p.SetPath(0, points);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(detection, 0.09f);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(detection2, 0.09f);
        
    }
}