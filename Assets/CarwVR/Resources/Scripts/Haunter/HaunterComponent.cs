using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;
public class HaunterComponent : MonoBehaviour
{
    [SerializeField] private LayerMask layerToCatch;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float maxDistance;
    [SerializeField] private float secondsToCatch;
    [SerializeField] private CompleteCircleSprite completeCircleSprite;
    private float elapsedSecondsLookingToCatchable;
    private RaycastHit hitInfo; 
    private void Update() {
        
        if (DetectCollisionWithCatchableObject()) {
            if (CheckIfCOuntDownOver()) {
                Destroy(hitInfo.collider.gameObject);
            }
            else {
                CompleteCircle();
            }
        }
        else {
            ResetCountDown();
            ResetCircle();
        }
       
    }
    private bool DetectCollisionWithCatchableObject() {
        return Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hitInfo, maxDistance, layerToCatch);
    }
    private bool CheckIfCOuntDownOver() {
        elapsedSecondsLookingToCatchable += Time.deltaTime;
        if (elapsedSecondsLookingToCatchable >= secondsToCatch) {
            ResetCountDown();
            return true;
        }
        return false;
    }
    private void ResetCountDown() {
        elapsedSecondsLookingToCatchable -= elapsedSecondsLookingToCatchable;
    }
    private void CompleteCircle() {
        completeCircleSprite.CompleteCircle(secondsToCatch);
    }
    private void ResetCircle() {
        completeCircleSprite.ResetCircle();
    }

}
