using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InvisibleObject : MonoBehaviour
{
    private Renderer _renderer;
    private Collider _collider;
    private Material initialMaterial;
    private XRGrabInteractable interactable;

    bool stayVisible;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider>();
        interactable = GetComponentInParent<XRGrabInteractable>();
        if(interactable != null) {
            interactable.selectEntered.AddListener(Grabbed);
        }
        initialMaterial = _renderer.material;
        Color _color = _renderer.material.color;
        float smoothness = _renderer.material.GetFloat("_Glossiness");
        float metallic = _renderer.material.GetFloat("_Metallic");
        _renderer.material = new Material(GameManager.Instance.invisibleMaterial);
        _renderer.material.color = _color;
        if(_collider != null)
            _collider.isTrigger = true;
        _renderer.material.SetFloat("_Glossiness", smoothness);
        _renderer.material.SetFloat("_Metallic", metallic);
        _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }

    private void Grabbed(SelectEnterEventArgs arg0) {
        stayVisible = true;
        _renderer.material = initialMaterial;
    }

    private void OnTriggerEnter(Collider other) {
        if (stayVisible) return;
        if (other.CompareTag("Gravity")) {
            if(_collider != null) {
                _collider.isTrigger = false;
                _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (stayVisible) return;
        if (other.CompareTag("Gravity")) {
            if(_collider != null) {
                 _collider.isTrigger = true;
                _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            }
        }
    }
}
