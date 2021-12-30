using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class DominantHandSetup : MonoBehaviour
{
    [SerializeField] private GameObject rightHand = null;
    [SerializeField] private GameObject leftHand = null;
    [SerializeField] private DeviceAttractor deviceAttractor = null;

    private void Start() {
        if (GameManager.Instance.dominantHandRight) {
            SetupRightHand();
        } else {
            SetupLeftHand();
        }
    }

    private void SetupLeftHand() {
        deviceAttractor.transform.SetParent(leftHand.transform);
        deviceAttractor.transform.localPosition = Vector3.zero;
        deviceAttractor.Setup();
    }

    private void SetupRightHand() {
        deviceAttractor.transform.SetParent(rightHand.transform);
        deviceAttractor.transform.localPosition = Vector3.zero;
        deviceAttractor.Setup();
    }
}
