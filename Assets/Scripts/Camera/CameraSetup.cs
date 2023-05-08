using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class CameraSetup : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        Camera.main.depthTextureMode = DepthTextureMode.Depth;
    }
}
