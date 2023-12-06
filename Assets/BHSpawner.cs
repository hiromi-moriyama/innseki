using System.Collections;
using UnityEngine;

public class BHSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject[] blackholePrefabs;  // Renamed the array variable for clarity
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

            // Choose a random black hole prefab from the array
            GameObject selectedBlackhole = blackholePrefabs[Random.Range(0, blackholePrefabs.Length)];

            // Instantiate the selected black hole at the spawner's position
            Instantiate(selectedBlackhole, transform.position, Quaternion.identity);
        }
    }
}
