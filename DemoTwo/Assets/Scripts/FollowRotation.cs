using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRotationBody : MonoBehaviour
{
    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Quaternion LookAtRotation = target.transform.rotation;

        Quaternion LookAtRotationOnly_Y = Quaternion.Euler(transform.rotation.eulerAngles.x, LookAtRotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        transform.rotation = LookAtRotationOnly_Y;
    }
}
