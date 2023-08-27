using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeipMover : MonoBehaviour
{
    //[SerializeField] private ParticleSystem _vfx;
    [SerializeField] private GameObject _vfx;
    [SerializeField] private Vector3 _direction;
    [SerializeField, Min(1)] private float _speed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<RomanSnake>(out RomanSnake roman))
        {
            Explode();
            InvertDirecton();
        }

        if (collision.TryGetComponent<Obstacle>(out Obstacle obstacle))
        {
            InvertDirecton();
        }
    }

    private void Explode()
    {
        GameObject vfx = Instantiate(_vfx, transform);
        vfx.transform.position = Vector3.zero;
        vfx.transform.parent = null;
        vfx.transform.position = transform.position;
    }

    private void InvertDirecton()
    {
            _direction.x = -_direction.x;
            _direction.y = -_direction.y;
    }
}
