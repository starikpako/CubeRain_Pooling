using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ColorChanger), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    private ColorChanger _colorChanger;
    private Rigidbody _rigidbody;
    private bool _hasTouchedPlatform;

    public event Action<Cube> Expired;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _hasTouchedPlatform = false;
        _colorChanger.ResetToDefault();
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasTouchedPlatform == false)
        {
            if (collision.gameObject.TryGetComponent(out Platform _))
            {
                _hasTouchedPlatform = true;
                _colorChanger.ApplyHitColor();
                StartCoroutine(DeactivateAfterDelay());
            }
        }
    }

    private IEnumerator DeactivateAfterDelay()
    {
        float delay = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);
        yield return new WaitForSeconds(delay);

        Expired?.Invoke(this);
        gameObject.SetActive(false);
    }
}