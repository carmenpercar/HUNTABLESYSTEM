using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;
public class HaunterComponent : MonoBehaviour
{
    [SerializeField] private LayerMask layerToCatch;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float maxDistance;


    private void Update() {
        
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, maxDistance, layerToCatch)) {
            Debug.Log("Catchable detected");
        }
       
    }

}
