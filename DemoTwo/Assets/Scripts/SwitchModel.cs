using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Animations.Rigging;
using UnityEngine.AI;
public class SwitchModel : MonoBehaviour
{
    // Start is called before the first frame update
    

    public XRNode inputSource;
    public XRController rightController;
    public InputHelpers.Button teleportActivationButton;
    public float activationThreshold = 0.1f;
    private bool holdtrigger = false;
    public Rig MyRig;
    public Animator myAnimator;
    public GameObject headPos;
    public GameObject mycamera;
    private GameObject TempCameraAnchorTp;
    public Transform TempCameraAnchorFp;
    public Transform CameraAnchorRoot;
    public TeleportationProvider teleportcontroller;
    public GameObject cammeraControllerRoot;
    public Transform AvatarRoot;
    private NavMeshAgent navMeshAgent;
    private MoveMode myMoveMode;
    public GameObject LeftHandRay;
    public XRInteractorLineVisual LeftLineVisual;
    public XRRayInteractor LeftLineInteractor;
    //private LocomotionController mylocomotionController;

    void Start()
    {
        myMoveMode = MoveMode.Instance;
        myMoveMode.setModetoTeleport();
        SetCameraLocation();
        navMeshAgent = GetComponent<NavMeshAgent>();
        //mylocomotionController = GetComponent<LocomotionController>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((!holdtrigger) && CheckIfActivated(rightController) )
        {
            
            myMoveMode.getNext();



            if (myMoveMode.isTeleport())
            {
                EnableHandIK();
                EnableUpperBodyAnimation(false);
                SetCameraLocation();
                controllteleportandNav();
            
            }
            else if (myMoveMode.isContinousMovement())
            {
                DisableHandIK();
                EnableUpperBodyAnimation(true);
                SetCameraLocation();
                controllteleportandNav();

            }
            else
            {
                DisableHandIK();
                EnableUpperBodyAnimation(true);
                SetCameraLocation();
                controllteleportandNav();
              

            }
        }

        if (!CheckIfActivated(rightController))
        {
            holdtrigger = false;
        }
    }

    void DisableHandIK()
    {
        MyRig.weight = 0;
    }

    void EnableHandIK()
    {
        MyRig.weight = 1;
    }

    public bool CheckIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationThreshold);
        if (isActivated)
        {
            holdtrigger = true;
        }

        return isActivated;
    }

    public bool EnableUpperBodyAnimation(bool enable)
    {
        if (enable)
        {
            myAnimator.SetLayerWeight(0, 1);
            myAnimator.SetLayerWeight(1, 0);
        }
        else {
            myAnimator.SetLayerWeight(1, 1);
            myAnimator.SetLayerWeight(0, 0);
        }
        return true;
    }
    public void SetCameraLocation(){
        if (myMoveMode.isTeleport())
        {
            cammeraControllerRoot.transform.rotation = AvatarRoot.transform.rotation;
            mycamera.transform.rotation = AvatarRoot.transform.rotation;
            headPos.transform.rotation = AvatarRoot.transform.rotation;

            cammeraControllerRoot.transform.SetParent(AvatarRoot);
            cammeraControllerRoot.transform.localPosition = new Vector3(0, 0, 0);
            //cammeraControllerRoot.transform.localRotation = new Vector3(0, 0, 0);
            
            mycamera.transform.SetParent(TempCameraAnchorFp);
            headPos.transform.SetParent(mycamera.transform);
            headPos.transform.localPosition = new Vector3(0, 0, -0.09f);
            deletCameraAnchor();

        }
        else if (myMoveMode.isContinousMovement())
        {
            headPos.transform.SetParent(TempCameraAnchorFp);
            headPos.transform.localPosition = new Vector3(0, 1.3f, -0.09f);
            ProduceCameraAnchor();
            mycamera.transform.SetParent(TempCameraAnchorTp.transform);
        }
        else {

            mycamera.transform.SetParent(TempCameraAnchorFp);
            headPos.transform.SetParent(AvatarRoot);
            headPos.transform.localPosition = new Vector3(0, 1.3f, -0.09f);
            ProduceCameraAnchor();
            cammeraControllerRoot.transform.SetParent(TempCameraAnchorTp.transform);
            cammeraControllerRoot.transform.localPosition = new Vector3(0,0,0);
        
        }
    }

    public void ProduceCameraAnchor() {

        if (!TempCameraAnchorTp){
            TempCameraAnchorTp = new GameObject("Camera Anchor");
        }
        if (myMoveMode.isContinousMovement())
        {
            TempCameraAnchorTp.transform.position = CameraAnchorRoot.position + new Vector3(0, 0.6f, -2);
        }
        else if (myMoveMode.isNavigation()) {
            TempCameraAnchorTp.transform.position = CameraAnchorRoot.position + new Vector3(0, 10f, -4);
        }
    }

    public void deletCameraAnchor() {
        if (TempCameraAnchorTp)
        {
            Destroy(TempCameraAnchorTp);
        }
    }

    public void controllteleportandNav() {

        if (myMoveMode.isContinousMovement())
        {
            teleportcontroller.enabled = false;
            navMeshAgent.enabled = false;
            ControllRayInteracte();
        }
        else if (myMoveMode.isNavigation()) {
            teleportcontroller.enabled = false;
            navMeshAgent.enabled = true;
            ControllRayInteracte();
            navMeshAgent.SetDestination(transform.position);
            //navMeshAgent.Stop();
        }
        else if (myMoveMode.isTeleport())
        {

            Debug.Log(navMeshAgent.destination);
            navMeshAgent.enabled = false;
            teleportcontroller.enabled = true;
            ControllRayInteracte();
        }
    }

    void ControllRayInteracte() {

        if (myMoveMode.isContinousMovement())
        {
            LeftHandRay.SetActive(false);
            LeftLineVisual.enabled = false; 
            LeftLineInteractor.enabled = false;
        }
        else {
            LeftHandRay.SetActive(true);
            LeftLineVisual.enabled = true;
            LeftLineInteractor.enabled = true;
        }
    }


}
