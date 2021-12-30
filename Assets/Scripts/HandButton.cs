using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class HandButton : XRBaseInteractable
{
    public UnityEvent OnPress = null;

    [SerializeField] private float yMin = 0.0f;
    [SerializeField] float yMax = 0.0f;
    [SerializeField] bool physicsButton;
    private bool previousPress = false;
    private AudioSource audioSource;

    private float previousHandHeight = 0.0f;
    private XRBaseInteractor hoverInteractor = null;

    protected override void Awake() {
        base.Awake();
        onHoverEntered.AddListener(StartPress);
        onHoverExited.AddListener(EndPress);
        audioSource = GetComponent<AudioSource>();
        if (physicsButton) {
            GetComponent<Collider>().isTrigger = true;
        }
    }

    private void OnDestroy() {
        onHoverExited.RemoveListener(StartPress);
        onHoverExited.RemoveListener(EndPress);
    }

    private void StartPress(XRBaseInteractor interactor) {
        hoverInteractor = interactor;
        previousHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
    }

    private void EndPress(XRBaseInteractor interactor) {
        hoverInteractor = null;
        previousHandHeight = 0.0f;

        previousPress = false;
        SetYPosition(yMax);
    }

    private void Start() {
        SetMinMax();
    }

    private void OnTriggerEnter(Collider other) {
        if(physicsButton && other.CompareTag("PhysicPusher")) {
            SetYPosition(yMin);
            OnPress.Invoke();
            if (audioSource != null) {
                audioSource.Play();
            }
            GetComponent<Collider>().isTrigger = false;
        }
    }

    private void SetMinMax() {
        Collider collider = GetComponent<Collider>();
        //yMin = transform.position.y - (collider.bounds.size.y * 0.3f);
        //yMax = transform.position.y;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase) {
        if (hoverInteractor) {
            float newHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
            float handDifference = previousHandHeight - newHandHeight;
            previousHandHeight = newHandHeight;

            float newPosition = transform.localPosition.y - handDifference;
            SetYPosition(newPosition);

            CheckPress();
        }
    }

    private float GetLocalYPosition(Vector3 position) {
        Vector3 localPosition = transform.parent.InverseTransformPoint(position);
        return localPosition.y;
    }

    private void SetYPosition(float position) {
        Vector3 newPosition = transform.localPosition;
        newPosition.y = Mathf.Clamp(position, yMin, yMax);
        transform.localPosition = newPosition;
    }

    private void CheckPress() {
        bool inPosition = InPosition();

        if (inPosition && inPosition != previousPress) {
            OnPress.Invoke();
            if (audioSource != null) {
                audioSource.Play();
            }
        }
        previousPress = inPosition;
    }

    private bool InPosition() {
        float inRange = Mathf.Clamp(transform.localPosition.y, yMin, yMin + 0.01f);
        return transform.localPosition.y == inRange;
    }
}
