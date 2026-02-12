using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class ColorChanger : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }
}