using UnityEngine;

public class DecoloredRenderer : MonoBehaviour {
    private Renderer _renderer;

    void Start() {
        _renderer = GetComponent<Renderer>();
        Color _color = _renderer.material.color;
        float smoothness = _renderer.material.GetFloat("_Glossiness");
        float metallic = _renderer.material.GetFloat("_Metallic");
        Vector2 tiling = _renderer.material.mainTextureScale;
        Texture texture = _renderer.material.mainTexture;
        _renderer.material = new Material(GameManager.Instance.decoloredMaterial);
        _renderer.material.color = _color;
        _renderer.material.SetFloat("_Glossiness", smoothness);
        _renderer.material.SetFloat("_Metallic", metallic);
        _renderer.material.mainTextureScale = tiling;
        if (texture != null) {
            _renderer.material.mainTexture = texture;
        }
    }
}
