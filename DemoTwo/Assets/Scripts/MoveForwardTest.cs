using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardTest : MonoBehaviour
{
    public int interpolationFramesCount = 100; // Number of frames to completely interpolate between the 2 positions
    int elapsedFrames = 1;
    public float interpolationRatio;
    // Start is called before the first frame update
    void Start()
    {
       interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = transform.position + new Vector3(0, 0, 0.5f);
        transform.position = Vector3.Lerp(transform.position, targetPosition, interpolationRatio);
    }
}
