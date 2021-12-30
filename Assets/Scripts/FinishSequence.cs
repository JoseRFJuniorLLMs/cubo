using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class FinishSequence : MonoBehaviour
{
    [SerializeField] Rigidbody xrRigRigidBody;
    [SerializeField] Transform camera;
    [SerializeField] AudioClip turnOff;
    [SerializeField] AudioClip hit;
    [SerializeField] ScreenFade screenFade;
    [SerializeField] ActionBasedContinuousMoveProvider movementProvider;
    AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void FinalSequence() {
        xrRigRigidBody.constraints = RigidbodyConstraints.None;
        xrRigRigidBody.AddForceAtPosition(-xrRigRigidBody.transform.right, camera.position);
        audioSource.PlayOneShot(hit);
        audioSource.PlayOneShot(turnOff);
        screenFade.fadeTime = 5;
        screenFade.FadeOut();
        movementProvider.enabled = false;
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene() {
        yield return new WaitForSeconds(12);
        SceneManager.LoadScene(1);
    }
}
