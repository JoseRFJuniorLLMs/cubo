using UnityEngine;
using System.Collections;

public class VRBody : MonoBehaviour {
    public Transform[] body;

    private Color playerColor;

    // Follow trackers only if it's our body
    void Update() {
        for (int i = 0; i < body.Length; i++) {
           //body[i].position = PlatformManager.instance.vrRigParts[i].position;
            //body[i].rotation = PlatformManager.instance.vrRigParts[i].rotation;
        }
    }
}

