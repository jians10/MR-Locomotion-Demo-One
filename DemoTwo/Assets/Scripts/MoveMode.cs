using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMode : MonoBehaviour
{

    public enum LocomotionMode
    {
        Teleport,
        ContinuousMovement,
        Navigation,
    }

    public LocomotionMode currentMoveMode;
    public static MoveMode Instance;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance ==null)
        {
            Instance = this;
        }
    }
    public void setModetoNavigation() {
        currentMoveMode = LocomotionMode.Navigation;
    }
    public void setModetoTeleport()
    {
        currentMoveMode = LocomotionMode.Teleport;
    }
    public void setModetoContinousMovement() {
        currentMoveMode = LocomotionMode.ContinuousMovement;
    }

    public bool isTeleport() {
        return currentMoveMode == LocomotionMode.Teleport;
    }

    public bool isNavigation()
    {
        return currentMoveMode == LocomotionMode.Navigation;
    }

    public bool isContinousMovement() {
        return currentMoveMode == LocomotionMode.ContinuousMovement;
    }

    public void getNext()
    {

        if (currentMoveMode == LocomotionMode.Teleport)
        {
            currentMoveMode = LocomotionMode.ContinuousMovement;
        }
        else if (currentMoveMode == LocomotionMode.ContinuousMovement)
        {
            currentMoveMode = LocomotionMode.Navigation;
        }
        else if (currentMoveMode == LocomotionMode.Navigation)
        {
            currentMoveMode = LocomotionMode.Teleport;
        }
    }




}
