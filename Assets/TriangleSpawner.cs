using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleSpawner : MonoBehaviour
{
    public GameObject trianglePrefab; // The prefab for your triangle
    public int numberOfTriangles = 10;
    public float minX = -15f;
    public float maxX = 15f;
    public float minY = -9f;
    public float maxY = 9f;

    private void Start()
    {
        for (int i = 0; i < numberOfTriangles; i++)
        {
            SpawnTriangle();
        }
    }

    public void SpawnTriangle()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
        GameObject newTriangle = Instantiate(trianglePrefab, spawnPosition, Quaternion.identity);
    }
}
