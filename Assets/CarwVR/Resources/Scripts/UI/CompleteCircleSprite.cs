using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompleteCircleSprite : MonoBehaviour
{
    [SerializeField] private float circleFillVelocity;
    private Image circleToComplete;
    private float minValue = 0.0f;
    private float maxValue = 1.0f;
    private void Start() {
        circleToComplete = GetComponent<Image>();
    }
    public void CompleteCircle(float secondsToComplete) {
        circleToComplete.fillAmount +=Time.deltaTime * maxValue / secondsToComplete;
        
    }
    public void ResetCircle() {
        circleToComplete.fillAmount = minValue;
    }
    
}
