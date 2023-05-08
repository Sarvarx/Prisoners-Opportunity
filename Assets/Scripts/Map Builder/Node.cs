using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public NodeShapes currentNodeShape;
    public Transform[] nodeShapes;
    public Vector2 vector;
    public int index;
    public float RotationZ = 0;

    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    Vector2[] HoldVectors = new Vector2[] 
    {   new Vector2(1, 0), 
        new Vector2(1, -1), 
        new Vector2(0, -1), 
        new Vector2(-1, -1), 
        new Vector2(-1, 0), 
        new Vector2(-1, 1), 
        new Vector2(0, 1), 
        new Vector2(1, 1),
        new Vector2(0, 0)
    };

    public void Start()
    {
        //if (nodeShapes.Length > 0) Instantiate(nodeShapes[0], transform);
    }
    public void SetShape(NodeShapes shape, Vector3 rotation)
    {
        Clear();
        currentNodeShape = shape;
        RotationZ = rotation.z;
        if (nodeShapes.Length > 0)
        {
            transform.localScale = new Vector3(100, 100, 100);
            transform.eulerAngles = rotation;
            GetComponent<MeshFilter>().sharedMesh = nodeShapes[((int)shape)].GetComponent<MeshFilter>().sharedMesh;
            GetComponent<MeshRenderer>().sharedMaterials = nodeShapes[((int)shape)].GetComponent<MeshRenderer>().sharedMaterials;
        }
    }
    public void Path()
    {
        NodesGenerator nodesGenerator = transform.parent.GetComponent<NodesGenerator>();
        Transform[] nodes = nodesGenerator.nodes;
        SetShape(NodeShapes.BOTTOM, new Vector3(-90, 0, 0));
        for (var i = 0; i < 8; i++)
        {
            int HoldIndex = GetHoldIndex((HoldPosition)i);
            if (!isOut(HoldIndex, 0, (nodesGenerator.NodesSize * nodesGenerator.NodesSize)-1))
            {
                if (nodes[HoldIndex])
                {
                    Node node = nodes[HoldIndex].GetComponent<Node>();
                    Vector2 holdVector = GetHoldVectors((HoldPosition)i);
                    Vector2 checkVector = new Vector2(node.vector.x + holdVector.x, node.vector.y + holdVector.y);
                    //print(holdVector);
                    //print(checkVector +" = "+vector);
                    if (Vector2.Equals(checkVector, vector))
                    {
                        print("Index:" + node.index);
                        SetSmartShape(node, (HoldPosition)i);
                    }
                    else
                    {
                        print("Position: Out");
                    }
                }
            }
            else
            {
                print("Position: Out");
            }
        }
    }
    public void Fix()
    {
        
    }

    void FixShape()
    {

    }
    public void Undo()
    {

    }
    public void Clear()
    {
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }
    }
    bool isOut(int value,int min, int max)
    {
        if (value < min || value > max)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SetSmartShape(Node node,HoldPosition holdNode)
    {
        if (node.currentNodeShape == NodeShapes.TOP)
        {
            if (holdNode == HoldPosition.Left)
            {
                node.SetShape(NodeShapes.LINEAR, new Vector3(-90, 0, 180));
            }
            else if (holdNode == HoldPosition.TopLeft)
            {
                node.SetShape(NodeShapes.INNER, new Vector3(-90, 0, 180));
            }
            else if (holdNode == HoldPosition.Top)
            {
                node.SetShape(NodeShapes.LINEAR, new Vector3(-90, 0, -90));
            }
            else if (holdNode == HoldPosition.TopRight)
            {
                node.SetShape(NodeShapes.INNER, new Vector3(-90, 0, -90));
            }
            else if (holdNode == HoldPosition.Right)
            {
                node.SetShape(NodeShapes.LINEAR, new Vector3(-90, 0, 0));
            }
            else if (holdNode == HoldPosition.BottomRight)
            {
                node.SetShape(NodeShapes.INNER, new Vector3(-90, 0, 0));
            }
            else if (holdNode == HoldPosition.Bottom)
            {
                node.SetShape(NodeShapes.LINEAR, new Vector3(-90, 0, 90));
            }
            else if (holdNode == HoldPosition.BottomLeft)
            {
                node.SetShape(NodeShapes.INNER, new Vector3(-90, 0, 90));
            }
        }
        else
        {
            if(node.currentNodeShape == NodeShapes.LINEAR)
            {
                if (holdNode == HoldPosition.Left)
                {
                    if (node.RotationZ == 90)
                    {
                        node.SetShape(NodeShapes.OUTER, new Vector3(-90, 0, 90));
                    }
                    else if(node.RotationZ == -90)
                    {
                        node.SetShape(NodeShapes.OUTER, new Vector3(-90, 0, 180));
                    }
                    else if(node.RotationZ == 0)
                    {
                        node.SetShape(NodeShapes.BOTTOM, new Vector3(-90, 0, 0));
                    }
                    
                }
                else if (holdNode == HoldPosition.TopLeft)
                {
                    if (node.RotationZ == 90)
                    {
                        node.SetShape(NodeShapes.OUTER, new Vector3(-90, 0, 90));
                    }
                    else if(node.RotationZ == 0)
                    {
                        node.SetShape(NodeShapes.OUTER, new Vector3(-90, 0, -90));
                    }
                }
                else if (holdNode == HoldPosition.Top)
                {
                    if (node.RotationZ == 180)
                    {
                        node.SetShape(NodeShapes.OUTER, new Vector3(-90, 0, 180));
                    }
                    else if(node.RotationZ == 0)
                    {
                        node.SetShape(NodeShapes.OUTER, new Vector3(-90, 0, -90));
                    }
                    else if (node.RotationZ == 90)
                    {
                        node.SetShape(NodeShapes.BOTTOM, new Vector3(-90, 0, 0));
                    }
                }
                else if (holdNode == HoldPosition.TopRight)
                {
                    if (node.RotationZ == 90)
                    {
                        node.SetShape(NodeShapes.OUTER, new Vector3(-90, 0, 0));
                    }
                    else if (node.RotationZ == 180)
                    {
                        node.SetShape(NodeShapes.OUTER, new Vector3(-90, 0, 180));
                    }
                }
                else if (holdNode == HoldPosition.Right)
                {
                    if (node.RotationZ == 90)
                    {
                        node.SetShape(NodeShapes.OUTER, new Vector3(-90, 0, 0));
                    }
                    else if (node.RotationZ == -90)
                    {
                        node.SetShape(NodeShapes.OUTER, new Vector3(-90, 0, -90));
                    }
                    else if (node.RotationZ == 180)
                    {
                        node.SetShape(NodeShapes.BOTTOM, new Vector3(-90, 0, 0));
                    }
                }
                else if (holdNode == HoldPosition.BottomRight)
                {
                    if (node.RotationZ == -90)
                    {
                        node.SetShape(NodeShapes.OUTER, new Vector3(-90, 0, -90));
                    } else if (node.RotationZ == 180)
                    {
                        node.SetShape(NodeShapes.OUTER, new Vector3(-90, 0, 90));
                    }
                    else if (node.RotationZ == -180)
                    {
                        node.SetShape(NodeShapes.OUTER, new Vector3(-90, 0, 90));
                    }
                }
                else if (holdNode == HoldPosition.Bottom)
                {
                    if (node.RotationZ == 0)
                    {
                        node.SetShape(NodeShapes.OUTER, new Vector3(-90, 0, 0));
                    }
                    else if (node.RotationZ == 180)
                    {
                        node.SetShape(NodeShapes.OUTER, new Vector3(-90, 0, 90));
                    }
                    else if (node.RotationZ == -90)
                    {
                        node.SetShape(NodeShapes.BOTTOM, new Vector3(-90, 0, 0));
                    }
                }
                else if (holdNode == HoldPosition.BottomLeft)
                {
                    if (node.RotationZ == 0)
                    {
                        node.SetShape(NodeShapes.OUTER, new Vector3(-90, 0, 0));
                    }
                    else if (node.RotationZ == -90)
                    {
                        node.SetShape(NodeShapes.OUTER, new Vector3(-90, 0, 180));
                    }
                }
            }else if (node.currentNodeShape == NodeShapes.INNER)
            {
                //INNER
                if (holdNode == HoldPosition.Left)
                {
                    if (node.RotationZ == 90 || node.RotationZ == 180)
                    {
                        node.SetShape(NodeShapes.LINEAR, new Vector3(-90, 0, 180));
                    }else if(node.RotationZ == -90)
                    {
                        node.SetShape(NodeShapes.OUTER, new Vector3(-90, 0, 180));
                    }
                    else if (node.RotationZ == 0)
                    {
                        node.SetShape(NodeShapes.OUTER, new Vector3(-90, 0, 90));
                    }
                }
                else if (holdNode == HoldPosition.TopLeft)
                {
                    if (node.RotationZ == -90)
                    {
                        node.SetShape(NodeShapes.LINEAR, new Vector3(-90, 0, -90));
                    }
                    else if (node.RotationZ == 90)
                    {
                        node.SetShape(NodeShapes.LINEAR, new Vector3(-90, 0, 180));
                    }else if(node.RotationZ == 0)
                    {
                        node.SetShape(NodeShapes.BOTTOM, new Vector3(-90, 0, 0));
                    }
                }
                else if (holdNode == HoldPosition.Top)
                {
                    if (node.RotationZ == -90 || node.RotationZ == 180)
                    {
                        node.SetShape(NodeShapes.LINEAR, new Vector3(-90, 0, -90));
                    }
                    else if (node.RotationZ == 90)
                    {
                        node.SetShape(NodeShapes.OUTER, new Vector3(-90, 0, 180));
                    }
                    else if (node.RotationZ == 0)
                    {
                        node.SetShape(NodeShapes.OUTER, new Vector3(-90, 0, -90));
                    }
                }
                else if (holdNode == HoldPosition.TopRight)
                {
                    if (node.RotationZ == 0)
                    {
                        node.SetShape(NodeShapes.LINEAR, new Vector3(-90, 0, 0));
                    }
                    else if (node.RotationZ == 180)
                    {
                        node.SetShape(NodeShapes.LINEAR, new Vector3(-90, 0, -90));
                    }
                }
                else if (holdNode == HoldPosition.Right)
                {
                    if (node.RotationZ == -90 || node.RotationZ == 0)
                    {
                        node.SetShape(NodeShapes.LINEAR, new Vector3(-90, 0, 0));
                    }
                }
                else if (holdNode == HoldPosition.BottomRight)
                {
                    if (node.RotationZ == -90)
                    {
                        node.SetShape(NodeShapes.LINEAR, new Vector3(-90, 0, 0));
                    }
                    else if (node.RotationZ == 90)
                    {
                        node.SetShape(NodeShapes.LINEAR, new Vector3(-90, 0, 90));
                    }
                }
                else if (holdNode == HoldPosition.Bottom)
                {
                    if (node.RotationZ == 0 || node.RotationZ == 90)
                    {
                        node.SetShape(NodeShapes.LINEAR, new Vector3(-90, 0, 90));
                    }
                }
                else if (holdNode == HoldPosition.BottomLeft)
                {
                    if (node.RotationZ == 0)
                    {
                        node.SetShape(NodeShapes.LINEAR, new Vector3(-90, 0, 90));
                    }
                    else if (node.RotationZ == 180)
                    {
                        node.SetShape(NodeShapes.LINEAR, new Vector3(-90, 0, -180));
                    }
                }
            }
        }
    }
    public int GetHoldIndex(HoldPosition hold)
    {
        NodesGenerator nodesGenerator = transform.parent.GetComponent<NodesGenerator>();
        int NodeSize = nodesGenerator.NodesSize;
        switch (hold)
        {
            case HoldPosition.Left: return index - 1;
            case HoldPosition.TopLeft:return index + (NodeSize - 1);
            case HoldPosition.Top: return index + (NodeSize + 0);
            case HoldPosition.TopRight: return index + (NodeSize + 1);
            case HoldPosition.Right: return index + 1;
            case HoldPosition.BottomRight: return index + (-NodeSize + 1);
            case HoldPosition.Bottom: return index + (-NodeSize);
            case HoldPosition.BottomLeft: return index + (-NodeSize - 1);
            default: return index;
        }
    }
    public Vector2 GetHoldVectors(HoldPosition hold)
    {
        NodesGenerator nodesGenerator = transform.parent.GetComponent<NodesGenerator>();
        int NodeSize = nodesGenerator.NodesSize;
        switch (hold)
        {
            case HoldPosition.Left: return HoldVectors[0];
            case HoldPosition.TopLeft: return HoldVectors[1];
            case HoldPosition.Top: return HoldVectors[2];
            case HoldPosition.TopRight: return HoldVectors[3];
            case HoldPosition.Right: return HoldVectors[4];
            case HoldPosition.BottomRight: return HoldVectors[5];
            case HoldPosition.Bottom: return HoldVectors[6];
            case HoldPosition.BottomLeft: return HoldVectors[7];
            default: return HoldVectors[8];
        }
    }
    public float getTrueRotation(float axis)
    {
        float Rotation;
        if (axis <= 180f)
        {
            return Rotation = axis;
        }
        else
        {
            return  Rotation = axis - 360f;
        }
    }
    private static float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
    }

    private static float UnwrapAngle(float angle)
    {
        if (angle >= 0)
            return angle;

        angle = -angle % 360;

        return 360 - angle;
    }
    public enum NodeShapes{
        TOP = 0,
        BOTTOM = 1,
        LINEAR = 2,
        INNER = 3,
        OUTER = 4
    }
    public enum HoldPosition
    {
        Left = 0,
        TopLeft = 1,
        Top = 2,
        TopRight = 3,
        Right = 4,
        BottomRight = 5,
        Bottom = 6,
        BottomLeft = 7
    }
}
