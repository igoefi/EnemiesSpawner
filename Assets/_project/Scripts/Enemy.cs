using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _deathTime = 10;

    private ObjectPool<Enemy> _pool;

    private void Update() =>
        transform.position += transform.forward * _speed * Time.deltaTime;

    public void SetPool(ObjectPool<Enemy> pool) =>
        _pool = pool;

    private void OnEnable()
    {
        StartCoroutine(DeathDelay());
    }

    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(_deathTime);
        _pool.Release(this);
    }
}
