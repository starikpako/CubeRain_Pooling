using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Color _hitColor = Color.red;

    private Renderer _renderer;
    private Color _defaultColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    public void ResetToDefault()
    {
        _renderer.material.color = _defaultColor;
    }

    public void ApplyHitColor()
    {
        _renderer.material.color = _hitColor;
    }
}