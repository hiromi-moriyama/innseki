using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private bool canSpawn = true;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn)
        {
            yield return wait;

            if (enemyPrefab.Length > 0)
            {
                int rand = Random.Range(0, enemyPrefab.Length);
                GameObject enemyToSpawn = enemyPrefab[rand];

                Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            }
        }
    }
}
