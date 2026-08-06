using System.Collections;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _enemySpawnPoints;
    [SerializeField] private float _spawnDelay;

    private bool _isSpawning = true;

    private void Start() =>
        StartCoroutine(Spawn());

    private IEnumerator Spawn()
    {
        while (_isSpawning)
        {
            _enemySpawnPoints[Random.Range(0, _enemySpawnPoints.Length - 1)].Spawn();
            yield return new WaitForSeconds(_spawnDelay);
        }
    }
}
