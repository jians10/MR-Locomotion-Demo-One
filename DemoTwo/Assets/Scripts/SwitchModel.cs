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
    //public Rig RightHand;



    void Start()
    {
        Teleport = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ((!holdtrigger)&&CheckIfActivated(rightController)) {
            Teleport = !Teleport;
        }

        if (!CheckIfActivated(rightController)) {
            holdtrigger = false;
        }

        if (Teleport) {
            EnableHandIK();
        } else {
            DisableHandIK();
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

}
