using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.AI;

public class LocomotionController : MonoBehaviour
{
    // Start is called before the first frame update

    public XRInteractorLineVisual MyXRRay;
    //public GameObject Controller;
    public Vector3 Destineposition;
    public InputHelpers.Button teleportButton;
    public float activationThreshold = 0.1f;
    public bool holdtrigger = false;
    public bool navigating = true;
    public XRController LeftController;
    
    private NavMeshAgent navMeshAgent;
    private Vector3 falsevalue;



    void Start()
    {
        holdtrigger = false;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navigating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((!holdtrigger) && DetectUserInput(LeftController))
        {
            navigating = true;
            GetSelectedLocation();
        }

        if (!DetectUserInput(LeftController))
        {
            holdtrigger = false;
        }
    }



    public bool DetectUserInput(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportButton, out bool isActivated, activationThreshold);
        if (isActivated)
        {
            holdtrigger = true;
        }

        return isActivated;
    }


    bool GetSelectedLocation()
    {
        //Vector3 normal;
        //int positionInLine;
        //bool isValidTarget;
        //MyXRRay.TryGetHitInfo(out Destineposition, out  normal, out positionInLine, out isValidTarget);

        //GameObject destinationobject = GameObject.Find("Cylinder");
        //if(Cylinder !=  )
        //if (!isValidTarget){
        //    Destineposition = falsevalue;
        //}


        GameObject destinationObject = MyXRRay.reticle;
        if (destinationObject)
        {
            Destineposition = destinationObject.transform.position;
        }

        return true;
    }
   
}
