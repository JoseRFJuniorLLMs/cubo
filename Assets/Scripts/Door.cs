using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Door : MonoBehaviour
{
    [SerializeField] HandButton openMechanism;
    private AudioSource audioSource;
    bool opened;
    [SerializeField] bool openUp;

    // Start is called before the first frame update
    void Start()
    {
        if(openMechanism != null) {
            openMechanism.OnPress.AddListener(OpenDoor);
        }
        audioSource = GetComponent<AudioSource>();
    }

    private void OnDestroy() {
        if (openMechanism != null) {
            openMechanism.OnPress.RemoveListener(OpenDoor);
        }
    }

    [ContextMenu("OpenDoor")]
    public void OpenDoor() {
        if(!opened)
            StartCoroutine(Co_OpenDoor());
    }

    IEnumerator Co_OpenDoor() {
        opened = true;
        if(audioSource != null) {
            audioSource.Play();
        }
        for (float i = 0; i < 1.5f; i += Time.deltaTime) {
            if (openUp) {
                transform.position += transform.up * Time.deltaTime * 2;
            } else {
                transform.position += transform.right * Time.deltaTime;
            }
            yield return null;
        }
    }
}
