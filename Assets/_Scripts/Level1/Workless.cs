using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Workless : MonoBehaviour
{
    [SerializeField] private float _approachSpeed;
    [SerializeField] private Vector3 _defaultPosition;
    [SerializeField] private float _speed;

    private Animator _animator;
    private Transform _target;
    private float _defaultPositionRadius = 0.1f;
    private bool _atPlace;

    private void Start()
    {
        _target = null;
        _atPlace = true;
        _defaultPosition = transform.position;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_target)
        {
            transform.position = Vector3.Lerp(transform.position, _target.position, _approachSpeed);
        }
        else if(_atPlace == false)
        {
            Vector3 direction = _defaultPosition - transform.position;
            transform.position += direction.normalized * Time.deltaTime * _speed;

            if ((_defaultPosition - transform.position).magnitude < _defaultPositionRadius)
            {
                _atPlace = true;
                _animator.SetBool("Run", false);
            }
        }
    }

    public void SetTarget(Transform target)
    {
        if (_atPlace == true)
        {
            _target = target;
            _atPlace = false;
            _animator.SetBool("Run", true);
        }
    }

    public void ResetTarget()
    {
        _target = null;
    }
}
