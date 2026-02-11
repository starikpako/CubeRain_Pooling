using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 0.1f;
    [SerializeField] private float spawnHeight = 15f;
    [SerializeField] private float areaWidth = 10f;
    [SerializeField] private float areaLength = 10f;

    private float _timer;

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            SpawnCube();
            _timer = spawnInterval;
        }
    }

    private void SpawnCube()
    {
        GameObject cube = ObjectPool.Instance.GetCube();

        float randomX = Random.Range(-areaWidth / 2f, areaWidth / 2f);
        float randomZ = Random.Range(-areaLength / 2f, areaLength / 2f);
        Vector3 spawnPos = new Vector3(randomX, spawnHeight, randomZ);

        cube.transform.position = spawnPos;
        cube.transform.rotation = Quaternion.identity;
    }
}