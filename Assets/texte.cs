using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class texte : MonoBehaviour
{

    PolygonCollider2D p;
    List<Vector2> points = new List<Vector2>();

    Vector2 lastNode;

    Vector2 detection;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
    }

    void Start()
    {
        p = GetComponent<PolygonCollider2D>();
        
        GetOriginalPollygonColliderNodes();
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

        Vector2 topLeft = new Vector2(v1.x / transform.localScale.x, .5f);
        Vector2 bottomLeft = new Vector2(v1.x / transform.localScale.x, v1.y - 0.3f);

        Vector2 bottomRight = new Vector2(v2.x / transform.localScale.x, v2.y - 0.3f);
        Vector2 topRight = new Vector2(v2.x / transform.localScale.x, .5f);

        Vector2[] nodes = { topLeft, bottomLeft, bottomRight, topRight };

        if (topLeft.x < lastNode.x)
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                points.Insert(points.Count - i, nodes[i]);
                lastNode = nodes[i];
            }
        }
        else
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                points.Insert(points.IndexOf(lastNode), nodes[i]);
                lastNode = nodes[i];
            }
        }

        p.SetPath(0, points);

        Debug.Log(lastNode);
        detection = lastNode;

        destroyGameObject = true;
        return;
    }

    void GetOriginalPollygonColliderNodes()
    {
        foreach (Vector2 vector in p.points) points.Add(vector);

        lastNode = points[points.Count-1];

        p.SetPath(0, points);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(detection, 0.1f);
    }
}