using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingLabel : MonoBehaviour
{
    [SerializeField, Min(0.1f)] private float _timer;

    private float _time;

    private void Start()
    {
        _time = 0;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        float scale = _time / _timer;

        if(_time < _timer)
        {
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
