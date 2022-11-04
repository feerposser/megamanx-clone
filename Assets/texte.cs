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

        OriginalPolygon();
    }

    Vector2 Create2DVector(float x, float y)
    {
        return new Vector2(x, y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Detection(collision.contacts[0].point, collision.contacts[1].point, collision.gameObject);
    }

    void Detection(Vector2 v1, Vector2 v2, GameObject g)
    {
        for (int i = 1; i < points.Count; i++)
        {
            int size = points.Count;
            Debug.Log(i + " : " +points[i].x + " > " + v1.x + " ? " + (points[i].x > v1.x));
            detection = v1;

            Vector2 v3aux = new Vector2(v1.x / transform.localScale.x, .5f);
            Vector2 v4aux = new Vector2(v2.x / transform.localScale.x, .5f);

            Vector2 v3 = new Vector2(v1.x / transform.localScale.x, v1.y - 0.3f);
            Vector2 v4 = new Vector2(v2.x / transform.localScale.x, v2.y - 0.3f);
            if (points[i].x > v3.x)
            {
                Debug.Log("b");
                points.Insert(i, v4aux);
                points.Insert(i, v4);
                points.Insert(i, v3);
                points.Insert(i, v3aux);
                p.SetPath(0, points);
                Destroy(g);
                return;
            }

            if (size < points.Count) return;
        }
    }

    void OriginalPolygon()
    {
        points.Add(Create2DVector(-.5f, .5f));

        /*
        // left
        points.Add(Create2DVector(0, .5f));
        points.Add(Create2DVector(0, -.2f));

        // rigth
        points.Add(Create2DVector(0.2f, -.2f));
        points.Add(Create2DVector(0.2f, .5f));
        */

        points.Add(Create2DVector(.5f, .5f));
        points.Add(Create2DVector(.5f, -.5f));
        points.Add(Create2DVector(-.5f, -.5f));

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