using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFlipper : MonoBehaviour
{
    [SerializeField] private bool _flippedIsLeft;

    private SpriteRenderer _renderer;
    private float _previousPositionX;
    private float _previousDirectionX;
    private bool _flipped;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _flipped = true;
        _previousPositionX = transform.position.x;
        _previousDirectionX = 1;
    }

    private void Update()
    {
        float directionX = transform.position.x - _previousPositionX;
        bool newFlip = _renderer.flipX;

        if (directionX < 0)
            if (_renderer.flipX != _flippedIsLeft)
                newFlip = _flippedIsLeft;
        
        if (directionX > 0)
            if (_renderer.flipX == _flippedIsLeft)
                newFlip = !_flippedIsLeft;

        if (_renderer.flipX != newFlip)
            _renderer.flipX = newFlip;

        _previousPositionX = transform.position.x;
    }
}
