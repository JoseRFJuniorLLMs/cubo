using UnityEngine;

//Class that moves the target of Avatar's IK corresponding to the position of the VR user
[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;
}

public class Avatar : MonoBehaviour
{
    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;

    public Transform headConstraint;
    Vector3 headBodyOffset;
    private float turnSmoothness = 3f;

    void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
    }
}

