using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ColorChanger), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;
    [SerializeField] private Color _hitColor = Color.red;

    private ColorChanger _colorChanger;
    private Rigidbody _rigidbody;
    private Color _defaultColor;
    private bool _hasTouchedPlatform;
    private ObjectPool _pool;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        _rigidbody = GetComponent<Rigidbody>();
        _defaultColor = GetComponent<Renderer>().material.color;
    }

    private void OnEnable()
    {
        _hasTouchedPlatform = false;
        _colorChanger.SetColor(_defaultColor);
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    public void Init(ObjectPool pool)
    {
        _pool = pool;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasTouchedPlatform == false)
        {
            if (collision.gameObject.TryGetComponent(out Platform platform))
            {
                _hasTouchedPlatform = true;
                _colorChanger.SetColor(_hitColor);
                StartCoroutine(DeactivateAfterDelay());
            }
        }
    }

    private IEnumerator DeactivateAfterDelay()
    {
        float delay = Random.Range(_minLifeTime, _maxLifeTime);
        yield return new WaitForSeconds(delay);
        _pool.ReturnCube(this);
    }
}