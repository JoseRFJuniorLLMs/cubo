using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflection : MonoBehaviour
{
    [SerializeField] Transform rig;
    [SerializeField] Transform rightHand;
    [SerializeField] Transform leftHand;

    [SerializeField] Transform human;
    [SerializeField] Transform centerPoint;
    [SerializeField] Transform rightTarget;
    [SerializeField] Transform leftTarget;

    public Vector3 trackingRotationOffsetRight;
    public Vector3 trackingRotationOffsetLeft;

    private bool inside;

    private void Update() {
        if (inside) {
            human.transform.position = centerPoint.position + (centerPoint.position - rig.position);
            leftTarget.transform.position = centerPoint.position + (centerPoint.position - leftHand.position);
            rightTarget.transform.position = centerPoint.position + (centerPoint.position - rightHand.position);

            rightTarget.transform.position = new Vector3(rightTarget.transform.position.x, rightHand.transform.position.y, rightTarget.transform.position.z);
            leftTarget.transform.position = new Vector3(leftTarget.transform.position.x, leftHand.transform.position.y, leftTarget.transform.position.z);

            leftTarget.transform.localRotation = leftHand.localRotation * Quaternion.Euler(trackingRotationOffsetLeft);
            rightTarget.transform.localRotation = rightHand.localRotation * Quaternion.Euler(trackingRotationOffsetRight);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            inside = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            inside = false;
        }
    }
}
