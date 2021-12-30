using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPoint : MonoBehaviour
{
    [HideInInspector] public bool grounded = true;
    public float currentSize = 0;
    private bool firstTimeGrabbed;

    Vector3 _mousePos, _smoothPoint;
    void Update()
    {
        if (firstTimeGrabbed) {
            if (grounded) {
                Grow();
            } else {
                Shrink();
            }
        } else {
            Shrink();
        }
    }

    private void Shrink() {
        if(transform.localScale.x > 0) {
            currentSize -= GameManager.Instance.sphereShrinkingSpeed * Time.deltaTime;
        } else {
            currentSize = 0;
        }
        SetValues();
    }
    private void Grow() {
        if (transform.localScale.x < GameManager.Instance.sphereMaxSize) {
            currentSize += GameManager.Instance.sphereGrowingSpeed * Time.deltaTime;
        } else {
            currentSize = GameManager.Instance.sphereMaxSize;
        }
        SetValues();
    }

    internal void Grabbed() {
        grounded = false;
        firstTimeGrabbed = true;
    }

    private void SetValues() {
        Mathf.Clamp(currentSize, 0, 100);
        Mathf.Clamp(GameManager.Instance._softness, 0, 100);

        transform.localScale = (new Vector3(currentSize, currentSize, currentSize));

        Shader.SetGlobalVector("GLOBALmask_Position", new Vector4(transform.position.x, transform.position.y, transform.position.z, 0));
        Shader.SetGlobalFloat("GLOBALmask_Radius", currentSize / 2);
        Shader.SetGlobalFloat("GLOBALmask_Softness", GameManager.Instance._softness);

        Shader.SetGlobalVector("_GLOBALMaskPosition", transform.position);
        Shader.SetGlobalFloat("_GLOBALMaskRadius", currentSize / 2);
        Shader.SetGlobalFloat("_GLOBALMaskSoftness", 0);
    }
}
