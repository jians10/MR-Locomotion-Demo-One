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
    public float thresholdistance;

    private LineRenderer mylineRender;
    private MoveMode myMoveMode;

    private void Awake()
    {
        myMoveMode = MoveMode.Instance;
    }

    void Start()
    {
       
        holdtrigger = false;
        navMeshAgent = GetComponent<NavMeshAgent>();
        mylineRender = GetComponent<LineRenderer>();
        navigating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!myMoveMode.isNavigation())
        {
            navigating = false;
            if (mylineRender.enabled)
            {
                mylineRender.enabled = false;
            }
            return;
        }

        if ((!holdtrigger) && DetectUserInput(LeftController))
        {
            navigating = true;
            GetSelectedLocation();
        }

        if (!DetectUserInput(LeftController))
        {
            holdtrigger = false;
        }

        if (!navigating) {
            if (mylineRender.enabled)
            {
                mylineRender.enabled = false;
            }
        }

        if (navigating)
        { 
            if (!mylineRender.enabled)
            {
                mylineRender.enabled = true;
            }

            navMeshAgent.destination = Destineposition;

            float remaindistance = Mathf.Abs(Vector3.Distance(navMeshAgent.destination, transform.position));
            drawpath();
            if (remaindistance < thresholdistance)
            {
                navMeshAgent.SetDestination(transform.position);
                navigating = false;
            }

        }
    }

    void drawpath() {
        mylineRender.positionCount = navMeshAgent.path.corners.Length;
        mylineRender.SetPosition(0, transform.position);
        if (navMeshAgent.path.corners.Length < 2) {
            return;
        }
        for (int i = 1; i < navMeshAgent.path.corners.Length; i++) {
            Vector3 pointPosition = new Vector3(navMeshAgent.path.corners[i].x, navMeshAgent.path.corners[i].y+1f, navMeshAgent.path.corners[i].z);
            mylineRender.SetPosition(i,pointPosition);
        
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
        GameObject destinationObject = MyXRRay.reticle;
        if (destinationObject)
        {
            if (CanReachPosition(destinationObject.transform.position))
            {
                Destineposition = destinationObject.transform.position;
            }
        }

        return true;
    }

    bool CanReachPosition(Vector3 position)
    {
        NavMeshPath path = new NavMeshPath();
        navMeshAgent.CalculatePath(position, path);
        return path.status == NavMeshPathStatus.PathComplete;
    }

}
