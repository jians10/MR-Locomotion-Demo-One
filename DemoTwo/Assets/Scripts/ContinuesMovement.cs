using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuesMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public XRNode inputSource;
    private Vector2 inputAxis;
    private CharacterController character;
    public float speed = 0f;
    public float gravity = -9.81f;
    public LayerMask groundLayer;
    public float additionalHeight = 0.2f;
    public GameObject cameraGameObject;
    private float fallingSpeed;

    void Start()
    {
        character = GetComponent<CharacterController>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<SwitchModel>().Teleport) {
            return;
        }



        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }
    void FixedUpdate() {

        if (GetComponent<SwitchModel>().Teleport)
        {
            return;
        }

        Quaternion headYaw = Quaternion.Euler(0, cameraGameObject.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        // Vector3 direction = new Vector3(inputAxis.x, 0, inputAxis.y);
        character.Move(direction * Time.fixedDeltaTime * speed);

        bool isGrounded = CheckIfGrounded();
        if (isGrounded)
            fallingSpeed = 0;
        else
            fallingSpeed += gravity * Time.fixedDeltaTime;

        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    bool CheckIfGrounded()
    {
        //tells us if on ground
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
    }

}
