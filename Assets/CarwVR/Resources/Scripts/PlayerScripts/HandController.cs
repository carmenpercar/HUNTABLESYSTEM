using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandController : MonoBehaviour
{
    [SerializeField] private GameObject controller;
    [SerializeField] private InputDeviceCharacteristics controllerCharacteristics;
    private List<InputDevice> devices = new List<InputDevice>();
    private InputDevice targetDevice;
    private GameObject spawnedController;
    private Animator handAnimator;
    private void Start() { 
        TryInitialize();
    }
    private void TryInitialize() {
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        if (devices.Count > 0) {
            targetDevice = devices[0];
        }
        spawnedController = Instantiate(controller, transform);
        handAnimator = spawnedController.GetComponent<Animator>();
    }
    private void Update() {

            UpdateAnimation();
  
    }
    private void UpdateAnimation() {
        if (targetDevice == null) {
            targetDevice = devices[0];
        }
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue)) {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else {
            handAnimator.SetFloat("Trigger", 0);
        }
        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue)) {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else {
            handAnimator.SetFloat("Grip", 0);
        }
    }
}
