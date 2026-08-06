using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _deathTime = 10;

    private ObjectPool<Enemy> _pool;
    private Transform _target;

    private void Update()
    {
        transform.LookAt(_target);
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    public void SetPool(ObjectPool<Enemy> pool) =>
        _pool = pool;

    public void SetTarget(Transform target) =>
        _target = target;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject == _target.gameObject)
            _pool.Release(this);
    }
}
