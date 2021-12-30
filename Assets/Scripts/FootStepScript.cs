using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;

public class FootStepScript : MonoBehaviour {
    [SerializeField] float stepRate = 0.5f;
    private float stepCounter;
    private Vector3 lastPos;
    [SerializeField] AudioClip[] footSteps;
    private AudioSource audioSource;
    private ActionBasedContinuousMoveProvider moveProvider;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        moveProvider = GetComponentInParent<ActionBasedContinuousMoveProvider>();
    }

    // Update is called once per frame
    void Update() {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        if (moveProvider.leftHandMoveAction.action.triggered) {
            if(lastPos != Vector3.zero) {
                stepCounter += Vector3.Distance(transform.position, lastPos);
            }
            if(stepCounter >= stepRate) {
                audioSource.pitch = 0.8f + Random.Range(-0.1f, 0.1f);
                audioSource.PlayOneShot(footSteps[Random.Range(0, footSteps.Length)], 0.9f);
                stepCounter = 0;
            }
            lastPos = transform.position;
        }
    }
}