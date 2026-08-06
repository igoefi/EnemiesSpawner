using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform[] _enemySpawnPoints;
    [SerializeField] private float _spawnDelay;

    private bool _isSpawning = true;
    private ObjectPool<Enemy> _pool;

    private void Start()
    {
        _pool = new(
            CreateCube,
            OnGetFromPool,
            OnRealesedToPool,
            (Enemy enemy) => Destroy(enemy.gameObject),
            false, 6, 6);

        StartCoroutine(Spawn());
    }

    private Enemy CreateCube()
    {
        var enemy = Instantiate(_enemyPrefab);
        enemy.SetPool(_pool);
        return enemy;
    }

    private void OnGetFromPool(Enemy enemy)
    {
        var rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        enemy.transform.SetPositionAndRotation(_enemySpawnPoints[Random.Range(0, _enemySpawnPoints.Length - 1)].position, rotation);
        enemy.gameObject.SetActive(true);
    }

    private void OnRealesedToPool(Enemy enemy) =>
        enemy.gameObject.SetActive(false);

    private IEnumerator Spawn()
    {
        while (_isSpawning)
        {
            _pool.Get();
            yield return new WaitForSeconds(_spawnDelay);
        }
    }
}
