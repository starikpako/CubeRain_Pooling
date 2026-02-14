using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _initialPoolSize = 30;

    private Queue<Cube> _poolQueue = new Queue<Cube>();

    private void Awake()
    {
        for (int i = 0; i < _initialPoolSize; i++)
        {
            CreateNewCube();
        }
    }

    private Cube CreateNewCube()
    {
        Cube cube = Instantiate(_cubePrefab, transform);
        cube.gameObject.SetActive(false);
        return cube;
    }

    public Cube GetCube()
    {
        Cube cube = _poolQueue.Count == 0 ? CreateNewCube() : _poolQueue.Dequeue();

        cube.Expired += OnCubeExpired;

        cube.gameObject.SetActive(true);
        return cube;
    }

    private void OnCubeExpired(Cube cube)
    {
        cube.Expired -= OnCubeExpired;
        _poolQueue.Enqueue(cube);
    }
}