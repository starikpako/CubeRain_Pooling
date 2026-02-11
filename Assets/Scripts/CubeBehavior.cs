using System.Collections;
using UnityEngine;

public class CubeBehavior : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Color hitColor = Color.red;
    [SerializeField] private float minLifeTime = 2f;
    [SerializeField] private float maxLifeTime = 5f;

    private bool _hasTouchedPlatform = false;
    private Renderer _objRenderer;
    private Rigidbody _rb;
    private Color _defaultColor;

    private void Awake()
    {
        _objRenderer = GetComponent<Renderer>();
        _rb = GetComponent<Rigidbody>();
        _defaultColor = _objRenderer.material.color;

        if (_rb == null)
        {
            Debug.LogError("�� ���� ����������� Rigidbody!");
        }
    }

    private void OnEnable()
    {
        _hasTouchedPlatform = false;
        _objRenderer.material.color = _defaultColor;

        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_hasTouchedPlatform)
        {
            _hasTouchedPlatform = true;
            _objRenderer.material.color = hitColor;

            float randomLifeTime = Random.Range(minLifeTime, maxLifeTime);
            StartCoroutine(LifeRoutine(randomLifeTime));
        }
    }

    private IEnumerator LifeRoutine(float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPool.Instance.ReturnCube(this.gameObject);
    }
}