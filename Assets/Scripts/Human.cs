using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField] Transform head;
    [SerializeField] Transform camera;

    private void Update() {
        head.LookAt(camera);
    }
}
