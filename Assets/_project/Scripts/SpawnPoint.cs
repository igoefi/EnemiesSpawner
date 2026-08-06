using UnityEngine;
using UnityEngine.Pool;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _target;

    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new(
            CreateCube,
            OnGetFromPool,
            OnRealesedToPool,
            (Enemy enemy) => Destroy(enemy.gameObject),
            false, 6, 6);
    }

    public void Spawn() =>
        _pool.Get();

    private Enemy CreateCube()
    {
        var enemy = Instantiate(_enemyPrefab);
        enemy.SetTarget(_target);
        enemy.SetPool(_pool);
        return enemy;
    }

    private void OnGetFromPool(Enemy enemy)
    {
        enemy.transform.position = transform.position;
        enemy.gameObject.SetActive(true);
    }

    private void OnRealesedToPool(Enemy enemy) =>
        enemy.gameObject.SetActive(false);
}
