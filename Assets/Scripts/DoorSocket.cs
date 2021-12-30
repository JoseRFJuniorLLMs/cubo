using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorSocket : MonoBehaviour
{
    private XRSocketInteractor socket;

    private void Start() {
        socket = GetComponent<XRSocketInteractor>();
        socket.selectEntered.AddListener(KeyPlaced);
    }

    private void KeyPlaced(SelectEnterEventArgs arg0) {
        StartCoroutine(DestroyKey(arg0.interactable.gameObject));
    }

    IEnumerator DestroyKey(GameObject key) {
        yield return new WaitForSeconds(1);
        Destroy(key);
    }
}
