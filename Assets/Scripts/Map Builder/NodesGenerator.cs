using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class NodesGenerator : MonoBehaviour
{
    public GameObject NodePrefab;
    public int NodesSize = 16;
    int NodesNumberX;
    int NodesNumberY;

    [HideInInspector] public Transform[] nodes;

    public void Generate()
    {
        Clear();
        NodesNumberX = NodesSize;
        NodesNumberY = NodesSize;
        nodes = new Transform[NodesNumberY * NodesNumberX];

        int nodeCount = 0;
        for (int y = 0; y < NodesNumberX; y++)
        {
            for (int x = 0; x < NodesNumberY; x++)
            {
                GameObject node = Instantiate(NodePrefab, transform);
                node.transform.position = new Vector3(x * 2, 0, y * 2);
                nodes[nodeCount] = node.transform;
                Node nodeCode = node.GetComponent<Node>();
                nodeCode.index = nodeCount;
                nodeCode.vector = new Vector2(x,y);
                nodeCode.SetShape(Node.NodeShapes.TOP, new Vector3(-90,0,0));
                nodeCount++;
            }
        }
    }

    public void Clear()
    {
        if (nodes == null) return;
        foreach (Transform child in nodes)
        {
            if(child) DestroyImmediate(child.gameObject);
        }
    }
}
