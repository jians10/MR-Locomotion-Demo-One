using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Animations.Rigging;
public class SwitchModel : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Teleport;
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
    //public Rig RightHand;




    void Start()
    {
        Teleport = true;
        SetCameraLocation(true);
    }

    // Update is called once per frame
    void Update()
    {
        if ((!holdtrigger) && CheckIfActivated(rightController)) {
            Teleport = !Teleport;
            if (Teleport)
            {
                EnableHandIK();
                EnableUpperBodyAnimation(false);
                SetCameraLocation(true);
            }
            else
            {
                DisableHandIK();
                EnableUpperBodyAnimation(true);
                SetCameraLocation(false);
            }
        }

        /*if (!Teleport)
        {
            mycamera.transform.rotation = Quaternion.Euler(0, 0, 0);
            //mycamera.transform.LookAt(headPos.transform);
        }*/

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
    public void SetCameraLocation(bool FP){
        if (FP)
        {
            mycamera.transform.SetParent(TempCameraAnchorFp);
            headPos.transform.SetParent(mycamera.transform);
            headPos.transform.localPosition = new Vector3(0, 0, -0.09f);
            deletCameraAnchor();
        }
        else {
            // Third Person
            headPos.transform.SetParent(TempCameraAnchorFp);
            headPos.transform.localPosition = new Vector3(0, 1.4f, -0.09f);
            ProduceCameraAnchor();
            mycamera.transform.SetParent(TempCameraAnchorTp.transform);
            //mycamera.transform.LookAt(headPos.transform);
        }
        

    }

    public void ProduceCameraAnchor() {
        if (!TempCameraAnchorTp)
        {
            TempCameraAnchorTp = new GameObject("Camera Anchor");
            TempCameraAnchorTp.transform.position = CameraAnchorRoot.position + new Vector3(0, 0.6f, -2);
        }
    }

    public void deletCameraAnchor() {
        if (TempCameraAnchorTp)
        {
            Destroy(TempCameraAnchorTp);
        }
        
    
    }



}
