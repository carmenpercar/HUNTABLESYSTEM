using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
public class ContinuousMovement : MonoBehaviour {

    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private XRNode inputSource;
    [SerializeField] private float offsetToGround = 0.01f;
    [SerializeField] private float additionallHeight = 0.2f;
    [SerializeField] LayerMask groundLayer;
    private float fallingSpeed;
    private XROrigin rig;
    private Vector2 inputAxis;
    private CharacterController character;

    private void Start() {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();
    }
    private void Update() {
        if (character == null) {
            character = GetComponent<CharacterController>();
        }
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }
    private void FixedUpdate() {
        CapsuleFollowHeadset();
        Quaternion headYaw = Quaternion.Euler(0, rig.Camera.transform.eulerAngles.y, 0);
        Vector3 direction =headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
        character.Move(direction*Time.fixedDeltaTime*speed);

        if (CheckIfGronded()) {
            fallingSpeed = 0.0f;
        }
        else {
            fallingSpeed += gravity * Time.fixedDeltaTime;
        }

        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    private bool CheckIfGronded() {
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + offsetToGround;
        bool hasHIt = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);

        return hasHIt;
    }
    private void CapsuleFollowHeadset() {
        character.height = rig.CameraInOriginSpaceHeight + additionallHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.Camera.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height/2 +  character.skinWidth, capsuleCenter.z);
    }
}
