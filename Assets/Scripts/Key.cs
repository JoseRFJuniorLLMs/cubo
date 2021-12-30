using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Key : MonoBehaviour
{
    private XRGrabInteractable interactable;
    private AudioSource audioSource;
    private bool stopRotating;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        audioSource = GetComponent<AudioSource>();
        if (interactable != null) {
            interactable.selectEntered.AddListener(Grabbed);
        }
    }

    private void Grabbed(SelectEnterEventArgs arg0) {
        stopRotating = true;
        audioSource.Play();
    }

    private void Update() {
        if (!stopRotating) {
            transform.Rotate(Vector3.up * 60 * Time.deltaTime);
        }
    }

    private void OnDestroy() {
        interactable.selectEntered.RemoveListener(Grabbed);
    }
}

