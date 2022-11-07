using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class texte : MonoBehaviour
{

    PolygonCollider2D p;
    List<Vector2> points = new List<Vector2>();

    Vector2 lastNode;

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
        bool destroyGameObject;

        Detection(collision.contacts[0].point, collision.contacts[1].point, out destroyGameObject);

        if (destroyGameObject) Destroy(collision.gameObject);
    }

    void Detection(Vector2 v1, Vector2 v2, out bool destroyGameObject)
    {
        /*
         * v1 = top left detection point
         * v2 = top right detection point
         * destroyGameObject = out true if created the impact
         */

        destroyGameObject = false;

        Vector2 topRight = new Vector2(v1.x / transform.localScale.x, .5f);
        Vector2 bottomRight = new Vector2(v1.x / transform.localScale.x, v1.y - 0.3f);

        Vector2 bottomLeft = new Vector2(v2.x / transform.localScale.x, v2.y - 0.3f);
        Vector2 topLeft = new Vector2(v2.x / transform.localScale.x, .5f);

        Vector2[] nodes = { topRight, bottomRight, bottomLeft, topLeft };

        if (topLeft.x < lastNode.x)
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                Debug.Log("<" + points.Count);
                points.Insert(points.Count - i, nodes[i]);
                lastNode = nodes[i];
            }
        } else
        {
            for (int i = 0; i < nodes.Length; i++) //(int i = nodes.Length-1; i >= 0; i--)
            {
                Debug.Log(">" + nodes[i]);
                points.Insert(points.IndexOf(lastNode), nodes[i]);
                lastNode = nodes[i];
            }
        }

        /*points.Insert(points.Count, topRight);
        points.Insert(points.Count, bottomRight);
        points.Insert(points.Count, bottomLeft);
        points.Insert(points.Count, topLeft);*/

        p.SetPath(0, points);

        destroyGameObject = true;
        return;

        /*for (int i = 0; i < points.Count; i++)
        {   
            // x collision point divided by x local scale (same size of the polygon collider) to fit into the polygon collider
            // increase of decrease x and y to increate the size of the hole

            Vector2 topRight = new Vector2(v1.x / transform.localScale.x, .5f);
            Vector2 bottomRight = new Vector2(v1.x / transform.localScale.x, v1.y - 0.3f);

            Vector2 bottomLeft = new Vector2(v2.x / transform.localScale.x, v2.y - 0.3f);
            Vector2 topLeft = new Vector2(v2.x / transform.localScale.x, .5f);

            if (points[i].y >= topLeft.y && points[i].x >= topLeft.x)
            {
                points.Insert(i+1, topRight);
                points.Insert(i+1, bottomRight);
                points.Insert(i+1, bottomLeft);
                points.Insert(i+1, topLeft);

                if (topLeft.x < lastNode.x) Debug.Log("<<>>");

                lastNode = topLeft;
                
                p.SetPath(0, points);

                destroyGameObject = true;
                return;
            }
        }*/
    }

    void OriginalPolygon()
    {
        foreach (Vector2 vector in p.points) points.Add(vector);

        lastNode = points[points.Count-1];

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