using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{
    [SerializeField] private XRController leftTeleportRay;
    [SerializeField] private XRController rightTeleportRay;
    [SerializeField] private InputHelpers.Button teleportActivationButton;
    [SerializeField] private float activationThreshold = 0.1f;

    private bool enableRightTeleport;
    private bool enableLeftTeleport;

    public bool EnableRightTeleport { get => enableRightTeleport; set => enableRightTeleport = value; }
    public bool EnableLeftTeleport { get => enableLeftTeleport; set => enableLeftTeleport = value; }

    // Update is called once per frame
    void Update()
    {
        if (leftTeleportRay) {
            leftTeleportRay.gameObject.SetActive(enableLeftTeleport && CheckIfActivated(leftTeleportRay));
        }
        if (rightTeleportRay) {
            rightTeleportRay.gameObject.SetActive(enableRightTeleport && CheckIfActivated(rightTeleportRay));
        }

    }

    public bool CheckIfActivated(XRController controller) {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationThreshold);
        return isActivated;
    }
}
