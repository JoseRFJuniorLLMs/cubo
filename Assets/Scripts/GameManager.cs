using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector3 Gravity {
        get {
            if (gravityInverted) {
                return new Vector3(0, gravitySpeed, 0);
            } else {
                return new Vector3(0, -gravitySpeed, 0);
            }
        }
    }
    public static GameManager Instance;

    [Header("Gravity Settings")]
    public float gravitySpeed = 100;
    public bool gravityInverted = false;
    public bool gravityInsideSphere = true;
    public bool randomPushUpwardsWhenGravityOff = true;
    public bool dominantHandRight = true;

    [Header("Light Device Settings")]
    public float sphereMaxSize = 4;
    public float sphereShrinkingSpeed = 8f;
    public float sphereGrowingSpeed = 4f;
    public float attractionSpeed = 18;
    public float attractTimer = 1.5f;

    [Header("Sphere Color Mask Settings")]
    public float _softness = 0.5f;
    public float _smoothSpeed = 40;
    public float _scaleFactor = 2;

    [Header("Materials")]
    public Material invisibleMaterial;
    public Material decoloredMaterial;

    void Awake()
    {
        if(Instance == null) {
            Instance = this;
        }
    }
}
