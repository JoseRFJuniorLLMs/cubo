using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class DeviceAttractor : MonoBehaviour
{
    private XRController controller = null;
    private XRDirectInteractor interactor = null;
    private LightEmitter device;
    private float counter;
    private bool interactorCanSelect = true;
    
    public void Setup() {
        controller = GetComponentInParent<XRController>();
        interactor = GetComponentInParent<XRDirectInteractor>();
        interactor.selectEntered.AddListener(interactorGrabbed);
        interactor.selectExited.AddListener(interactorReleased);
        device = FindObjectOfType<LightEmitter>();
    }

    private void interactorReleased(SelectExitEventArgs arg0) {
        interactorCanSelect = true;
    }

    private void interactorGrabbed(SelectEnterEventArgs arg0) {
        interactorCanSelect = false;
    }

    void Update()
    {
        CheckPointer();
    }
    private void CheckPointer() {
        if (interactorCanSelect) {
            if (controller.inputDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
                AttractDevice(gripValue);
        }
    }

    private void AttractDevice(float value) {
        if(value > 0.5f) {
            counter += Time.deltaTime;
        } else {
            counter = 0;
        }
        if(counter >= GameManager.Instance.attractTimer) {
            device.transform.position = Vector3.Lerp(device.transform.position, transform.position, Time.deltaTime * GameManager.Instance.attractionSpeed);
            counter = GameManager.Instance.attractTimer;
        }
        controller.SendHapticImpulse(Mathf.InverseLerp(0, GameManager.Instance.attractTimer, counter/3), 0.01f);
    }
}
