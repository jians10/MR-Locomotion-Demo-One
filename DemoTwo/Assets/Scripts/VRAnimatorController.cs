using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;

public class VRAnimatorController : MonoBehaviour
{
    public float speedTreshold = 0.1f;
    [Range(0,1)]
    public float smoothing = 1;
    private Animator animator;
    private Vector3 previousPos;
    public XROrigin vrRig;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //vrRig = GetComponent<VRRig>();
        previousPos = vrRig.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Compute the speed
        Vector3 headsetSpeed = (vrRig.transform.position - previousPos) / Time.deltaTime;
        headsetSpeed.y = 0;
        //Local Speed
        Vector3 headsetLocalSpeed = transform.InverseTransformDirection(headsetSpeed);
        previousPos = vrRig.transform.position;

        //Set Animator Values
        float previousDirectionX = animator.GetFloat("DirectionX");
        float previousDirectionY = animator.GetFloat("DirectionY");


        Debug.Log(headsetLocalSpeed.x);
        Debug.Log(headsetLocalSpeed.y);


        animator.SetBool("isMoving", headsetLocalSpeed.magnitude > speedTreshold);
        animator.SetFloat("DirectionX", Mathf.Lerp(previousDirectionX, Mathf.Clamp(headsetLocalSpeed.x,-1,1),smoothing));
        animator.SetFloat("DirectionY", Mathf.Lerp(previousDirectionY, Mathf.Clamp(headsetLocalSpeed.z, -1, 1),smoothing));
    }
}
