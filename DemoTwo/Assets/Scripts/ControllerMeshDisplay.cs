using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMeshDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ControllerMesh;
    private GameObject spawnedController;
    private MoveMode myMoveMode;

    private void Awake()
    {
        myMoveMode = MoveMode.Instance;
    }

    void Start()
    {
       
        spawnedController = Instantiate(ControllerMesh, transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (myMoveMode.isNavigation())
        {
            spawnedController.active = true;

        }
        else {

            spawnedController.active = false;
        
        }
    }
}
