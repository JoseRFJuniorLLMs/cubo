using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LightEmitter : MonoBehaviour
{
    private XRGrabInteractable interactable;
    private AudioSource audioSource;
    [SerializeField] ColorPoint colorPoint;

    void Awake()
    {
        interactable = GetComponentInParent<XRGrabInteractable>();
        interactable.selectEntered.AddListener(Grabbed);
        interactable.selectExited.AddListener(Released);
        audioSource = GetComponent<AudioSource>();
    }
    private void Update() {
        audioSource.volume = Mathf.Clamp01(colorPoint.currentSize);
    }

    private void Released(SelectExitEventArgs arg0) {
        colorPoint.grounded = true;
    }

    private void Grabbed(SelectEnterEventArgs arg0) {
        colorPoint.Grabbed();
        if (!audioSource.isPlaying) {
            audioSource.Play();
        }
    }

    [ContextMenu("Activate")]
    public void Debug_Activate() {
        colorPoint.Grabbed();
        if (!audioSource.isPlaying) {
            audioSource.Play();
        }
        colorPoint.grounded = true;
    }
    [ContextMenu("Deactivate")]
    public void Debug_Deactivate() {
        colorPoint.grounded = false;
    }
}
