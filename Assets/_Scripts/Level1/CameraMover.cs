using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _offsetX;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _koeff;

    private void LateUpdate()
    {
        Vector3 offset = _target.transform.position - transform.position;
        Vector3 position = transform.position;

        if(offset.x > _offsetX)
        {
            position.x = _target.transform.position.x - _offsetX;
        }
        else if (offset.x < -_offsetX)
        {
            position.x = _target.transform.position.x + _offsetX;
        }

        if (offset.y > _offsetY)
        {
            position.y = _target.transform.position.y - _offsetY;
        }
        else if (offset.y < -_offsetY)
        {
            position.y = _target.transform.position.y + _offsetY;
        }

        transform.position = Vector3.Lerp(transform.position, position, _koeff);
    }
}
