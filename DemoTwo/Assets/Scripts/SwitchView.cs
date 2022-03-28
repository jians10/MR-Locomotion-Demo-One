using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SwitchView : MonoBehaviour
{

    public Transform FPView;
    public Transform TPView;
    private Transform CurrentView;
    // Start is called before the first frame update
    void Start()
    {
        CurrentView = FPView;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = FPView.position;
    }

    public void setView(bool FP) {
        if (FP)
        {
            CurrentView = FPView;
        }
        else {
            CurrentView = TPView;
        
        }
    }


}
