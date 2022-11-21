using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class texte : MonoBehaviour
{

    public class Node
    {
        public int id;
        public Vector2 topLeft;
        public Vector2 bottomLeft;
        public Vector2 bottomRight;
        public Vector2 topRight;

        public Node(Vector2[] nodes)
        {
            topLeft = nodes[0];
            bottomLeft = nodes[1];
            bottomRight = nodes[2];
            topRight = nodes[3];
        }

        public Node() { }

        public override string ToString()
        {
            return "id: " + id + "\n\ttop left: " + topLeft + "\ttop right: " + topRight;
        }
    }

    public class ImpactConstructor
    {
        LinkedList<Node> nodes = new LinkedList<Node>();

        // points that create the borders of the square
        public Vector2 topLeft, topRight, bottomLeft, bottomRight;

        public ImpactConstructor(Node node)
        {
            topLeft = node.topLeft;
            bottomLeft = node.bottomLeft;
            bottomRight = node.bottomRight;
            topRight = node.topRight;
        }

        public void InsertNode(Node newNode)
        {
            newNode.id = nodes.Count + 1;
            LinkedListNode<Node> node;

            if (nodes.Count >= 2)
            {
                node = nodes.First;
                foreach (Node _ in nodes)
                {
                    if (node.Next == null) // if the loop reach the last element, the node must be before the first of after the last
                    {
                        LinkedListNode<Node> first = nodes.First;
                        if (NewNodeIsBefore(first, newNode))
                        {
                            nodes.AddBefore(first, newNode);
                            return;
                        }

                        LinkedListNode<Node> last = nodes.Last;
                        if (NewNodeIsAfter(last, newNode))
                        {
                            nodes.AddAfter(last, newNode);
                            return;
                        }
                    }

                    // if new node is between the actual node and the next, add it between
                    if (NewNodeIsAfter(node, newNode) && NewNodeIsBefore(node.Next, newNode))
                    {
                        nodes.AddAfter(node, newNode);
                        return;
                    }

                    node = node.Next; // set to the next node
                }
            }
            else
            {
                if (nodes.Count == 0) 
                    nodes.AddFirst(newNode); // if is the first one, add first
                else if (nodes.Count == 1) // if is the second one
                {
                    node = nodes.First;
                    if (NewNodeIsBefore(node, newNode)) // if is smaller than the first,
                        nodes.AddBefore(node, newNode); // add before him
                    else if (NewNodeIsAfter(node, newNode)) // if is bigger than the first
                        nodes.AddAfter(node, newNode); // add after him
                }
            }  
        }

        private bool NewNodeIsBefore(LinkedListNode<Node> node, Node newNode)
        {
            return node.Value.topLeft.x >= newNode.topRight.x;
        }

        private bool NewNodeIsAfter(LinkedListNode<Node> node, Node newNode)
        {
            return node.Value.topRight.x <= newNode.topLeft.x;
        }

        public Vector2[] ToArray()
        {
            // return the linked list nodes as a array of vectors
            // start to the left to right

            List<Vector2> vectorList = new List<Vector2>();

            LinkedListNode<Node> node = nodes.First;

            foreach (Node _ in nodes)
            {
                Node value = node.Value;

                vectorList.Add(value.topLeft);
                vectorList.Add(value.bottomLeft);
                vectorList.Add(value.bottomRight);
                vectorList.Add(value.topRight);

                if (node.Next == null) return vectorList.ToArray();
                else node = node.Next;
            }

            return vectorList.ToArray();
        }

        public void Show()
        {
            int i = 1;
            Debug.Log("----- start list");
            foreach (Node node in nodes)
            {
                Debug.Log("\t--" + i);
                Debug.Log("\t" + node);
                i++;
            }
            Debug.Log("----- end list");
        }
    }


    PolygonCollider2D p;
    List<Vector2> points = new List<Vector2>();

    Vector2 lastNode;

    Vector2 detection;

    ImpactConstructor l;

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

        Vector2[] v1 = { points[0], points[1], points[2], points[3] };

        Node a = new Node(v1);

        l = new ImpactConstructor(a);
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

        Node hotspot = new Node(nodes);

        l.InsertNode(hotspot);

        points.Clear();

        points.Add(l.topLeft);

        foreach (Vector2 node in l.ToArray()) points.Add(node);

        points.Add(l.topRight);
        points.Add(l.bottomRight);
        points.Add(l.bottomLeft);

        p.SetPath(0, points);

        detection = lastNode;

        destroyGameObject = true;

        //l.Show();

        return;
    }

    void GetOriginalPollygonColliderNodes()
    {
        foreach (Vector2 vector in p.points) points.Add(vector);

        lastNode = points[points.Count-1];

        p.SetPath(0, points);
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(detection, 0.1f);
    }*/
}