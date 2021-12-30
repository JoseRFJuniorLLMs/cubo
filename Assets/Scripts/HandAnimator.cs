using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HandAnimator : MonoBehaviour
{
    public float speed = 5.0f;
    public XRController controller = null;
    [SerializeField] private bool leftHand;
    private Animator animator = null;

    private readonly List<Finger> gripfingers = new List<Finger>()
    {
        new Finger(FingerType.Middle),
        new Finger(FingerType.Ring),
        new Finger(FingerType.Pinky)
    };

    private readonly List<Finger> pointFingers = new List<Finger>
    {
        new Finger(FingerType.Index),
        new Finger(FingerType.Thumb)
    };

    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponentInParent<XRController>();
    }

    private void Update()
    {
        if (controller == null) return;
        // Store input
        CheckGrip();
        CheckPointer();

        // Smooth input values
        SmoothFinger(pointFingers);
        SmoothFinger(gripfingers);

        // Apply smoothed values
        AnimateFinger(pointFingers);
        AnimateFinger(gripfingers);
    }

    private void CheckGrip()
    {
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            SetFingerTargets(gripfingers, gripValue);
    }

    private void CheckPointer()
    {
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float gripValue))
            SetFingerTargets(pointFingers, gripValue);
    }

    private void SetFingerTargets(List<Finger> fingers, float value)
    {
        foreach (Finger finger in fingers)
            finger.target = value;
    }

    private void SmoothFinger(List<Finger> fingers)
    {
        foreach(Finger finger in fingers)
        {
            float time = speed * Time.unscaledDeltaTime;
            finger.current = Mathf.MoveTowards(finger.current, finger.target, time);
        }
    }

    private void AnimateFinger(List<Finger> fingers)
    {
        foreach (Finger finger in fingers)
            AnimateFinger(finger.type.ToString(), finger.current);
    }

    private void AnimateFinger(string finger, float blend)
    {
        animator.SetFloat(finger, blend);
    }
}