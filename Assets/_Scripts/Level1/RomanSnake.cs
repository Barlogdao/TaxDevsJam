using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RomanSnake : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _distance;
    [SerializeField] private int _maxTailLen;

    private int _tailSegments;
    private List<Transform> _tail;
    private List<Workless> _followers;
    private List<Vector2> _route;
    private Vector2 _direction;
    private int _followersCount;

    void Start()
    {
        _followersCount = 0;
        _tailSegments = 0;
        _direction = Vector2.up;
        _tail = new List<Transform>();
        _followers = new List<Workless>();
        _route = new List<Vector2>();
        Vector3 defaultBonescale = new Vector3(0.2f, 0.2f, 0.2f);

        for(int i = 0; i < _maxTailLen; i++)
        {
            GameObject bone = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            bone.transform.localScale = defaultBonescale;
            bone.transform.position = Vector2.down * _distance * (i + 1f);
            bone.transform.position = transform.position + bone.transform.position;
            //bone.GetComponent<MeshRenderer>().enabled = false;
            _tail.Add(bone.transform);
        }
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            SetDirection(Vector2.right);
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            SetDirection(Vector2.left);
        }
        else if(Input.GetKeyDown(KeyCode.W))
        {
            SetDirection(Vector2.up);
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            SetDirection(Vector2.down);
        }

        Transform target = transform;

        foreach(var bone in _tail)
        {
            Vector3 offset = target.position - bone.transform.position;

            if (offset.magnitude > _distance)
            {
                bone.position = target.position - offset.normalized * _distance;
            }

            target = bone;
        }

        transform.Translate(_direction * Time.deltaTime * _speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Workless>(out Workless workless))
        {
            if(_followersCount < _maxTailLen)
            {
                if (_followers.Contains(workless) == false)
                {
                    workless.SetTarget(_tail[_followersCount].transform);
                    _followers.Add(workless);
                    _followersCount++;
                    // Debug.Log(_followersCount);
                }
            }
        }

        if(collision.TryGetComponent<DamageArea>(out DamageArea damageArea))
        {
            Debug.Log("Lose tail");
            LoseTail();
        }
    }

    public void LoseTail()
    {
        if (_followersCount == 0)
            return;

        _followersCount--;
        _followers[_followersCount].ResetTarget();
        _followers.RemoveAt(_followersCount);
    }

    private void SetDirection(Vector2 direction)
    {
        if (_direction == direction)
            return;

        _direction = direction;
        AddRoutePoint(transform.position);
    }

    private void AddRoutePoint(Vector2 point)
    {
        _route.Insert(0, point);
    }
}
