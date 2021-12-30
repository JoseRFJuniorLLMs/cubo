using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class BraceletPowers : MonoBehaviour
{
    [SerializeField] GameObject lantern;
    [SerializeField] GameObject bracelet;
    [SerializeField] XRController leftController;
    [SerializeField] CustomGravity customGravity;
    bool hasBracelet = false;
    bool transformControlActive = false;
    bool playerHoldingLantern = false;
    Vector3 initialLanternPosition;
    Vector3 lanternPosition;
    Vector3 initialBraceletPosition;
    Vector3 braceletPosition;
    float varianceX;
    float varianceY;
    float varianceZ;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hasBracelet && leftController.inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            if (triggerValue > 0.5)
            {
                customGravity.enabled = false;
                GetVariance();
                TransformControl();
                if (!transformControlActive)
                {
                    GetInitialPositions();
                    transformControlActive = true;
                }
            }
            else
            {
                transformControlActive = false;
                customGravity.enabled = true;
            }
        }
    }

    public void SetHasBracelet()
    {
        hasBracelet = !hasBracelet;
        leftController.SendHapticImpulse(1, 0.2f);
        print("Bracelet attached/detached");
    }

    void SetPlayerHoldingLanternActive()
    {
        playerHoldingLantern = !playerHoldingLantern;
    }

    public void GetInitialPositions()
    {
            // GetTransformOfLantern();
            GetTransformOfBracelet();
            print(initialBraceletPosition);
    }

/*    void GetTransformOfLantern()
    {
        initialLanternPosition = lantern.transform.position;
    }*/

    void GetTransformOfBracelet()
    {
        initialBraceletPosition = bracelet.transform.position;
    }

    void GetVariance()
    {
        varianceX = bracelet.transform.position.x - initialBraceletPosition.x;
        varianceY = bracelet.transform.position.y - initialBraceletPosition.y;
        varianceZ = bracelet.transform.position.z - initialBraceletPosition.z;
    }

    void TransformControl()
    { 
        lantern.transform.position += new Vector3(varianceX/5, varianceY/5, varianceZ/5);
    }
}
