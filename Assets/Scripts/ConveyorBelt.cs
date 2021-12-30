using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    private Rigidbody body = null;
    private Renderer _renderer;

    private void Start() {
        _renderer = GetComponent<Renderer>();
    }

    private void Update() {
        if(body != null) {
            body.transform.position += -transform.right * Time.deltaTime / 3;
        }
        _renderer.material.mainTextureOffset += new Vector2(1f, 0) * Time.deltaTime / 3; ;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<LightEmitter>() != null) {
            body = other.attachedRigidbody;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<LightEmitter>() != null) {
            body = null;
        }
    }
}
