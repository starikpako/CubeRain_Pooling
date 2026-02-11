using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [Header("Настройки Пула")]
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private int initialPoolSize = 30;

    private Queue<GameObject> poolQueue = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewCube();
        }
    }

    private GameObject CreateNewCube()
    {
        GameObject cube = Instantiate(cubePrefab, transform);
        cube.SetActive(false); // Скрываем сразу
        poolQueue.Enqueue(cube);
        return cube;
    }

    public GameObject GetCube()
    {
        if (poolQueue.Count == 0) CreateNewCube();

        GameObject cube = poolQueue.Dequeue();
        cube.SetActive(true);
        return cube;
    }

    public void ReturnCube(GameObject cube)
    {
        cube.SetActive(false);
        poolQueue.Enqueue(cube); 
    }
}