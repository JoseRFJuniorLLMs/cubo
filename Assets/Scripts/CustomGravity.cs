using System.Collections;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    private Rigidbody rigidBody;
    private bool affectedByGravity;
    public bool forceGravity;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        GravityOn();
    }

    void Update()
    {
        if (affectedByGravity || forceGravity) {
            rigidBody.AddForce(GameManager.Instance.Gravity * Time.deltaTime, ForceMode.Force);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Gravity")) {
            if (GameManager.Instance.gravityInsideSphere) {
                GravityOn();
            } else {
                GravityOff();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Gravity")) {
            if (GameManager.Instance.gravityInsideSphere) {
                GravityOff();
            } else {
                GravityOn();
            }
        }
    }

    private void GravityOn() {
        affectedByGravity = true;
    }

    private void GravityOff() {
        affectedByGravity = false;
        if (GameManager.Instance.randomPushUpwardsWhenGravityOff) {
            StartCoroutine(ForceUp());
        }
    }

    IEnumerator ForceUp() {
        yield return new WaitForSeconds(Random.Range(0.3f, 0.6f));
        rigidBody.AddForce(Vector3.up * Random.Range(2f, 6f));
    }
}
