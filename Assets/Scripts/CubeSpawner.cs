using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private const float CenterDivider = 2f;

    [SerializeField] private CubePool _pool;
    [SerializeField] private float _spawnInterval = 0.1f;
    [SerializeField] private float _spawnHeight = 15f;
    [SerializeField] private float _areaWidth = 10f;
    [SerializeField] private float _areaLength = 10f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        var wait = new WaitForSeconds(_spawnInterval);

        while (enabled)
        {
            Spawn();
            yield return wait;
        }
    }

    private void Spawn()
    {
        Cube cube = _pool.GetCube();

        float halfWidth = _areaWidth / CenterDivider;
        float halfLength = _areaLength / CenterDivider;

        float x = Random.Range(-halfWidth, halfWidth);
        float z = Random.Range(-halfLength, halfLength);

        cube.transform.position = new Vector3(x, _spawnHeight, z);
        cube.transform.rotation = Quaternion.identity;
    }
}