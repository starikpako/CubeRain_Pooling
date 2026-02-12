using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
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
        cube.Init(this);
        cube.gameObject.SetActive(false);
        _poolQueue.Enqueue(cube);
        return cube;
    }

    public Cube GetCube()
    {
        if (_poolQueue.Count == 0)
        {
            CreateNewCube();
        }

        Cube cube = _poolQueue.Dequeue();
        cube.gameObject.SetActive(true);
        return cube;
    }

    public void ReturnCube(Cube cube)
    {
        cube.gameObject.SetActive(false);
        _poolQueue.Enqueue(cube);
    }
}