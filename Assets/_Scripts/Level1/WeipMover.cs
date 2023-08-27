using UnityEngine;

public class WeipMover : MonoBehaviour
{
    //[SerializeField] private ParticleSystem _vfx;
    [SerializeField] private GameObject _vfx;
    [SerializeField] private Vector3 _direction;
    [SerializeField, Min(1)] private float _speed;

    void Start()
    {
        switch (Random.Range(0, 4))
        {
            case 0: _direction =  new Vector3(-1,0,0); break;
            case 1: _direction = new Vector3(1,0,0); break;
            case 2: _direction = new Vector3(0,1,0); break;
            case 3: _direction = new Vector3(0,-1,0); break;
        }

    }

    public void Init(Level1Timer _timer)
    {
        _timer.Elapsed += OnGameOver;
         void OnGameOver()
        {
            _timer.Elapsed -= OnGameOver;

            KillWeip();
        }
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
            KillWeip();
        }

        if (collision.TryGetComponent<Obstacle>(out Obstacle obstacle))
        {
            InvertDirecton();
        }
    }

    private void KillWeip()
    {
        Explode();
        Destroy(gameObject);
    }

    private void Explode()
    {
        GameObject vfx = Instantiate(_vfx, transform.position,Quaternion.identity);
        //vfx.transform.position = Vector3.zero;
        //vfx.transform.parent = null;
        //vfx.transform.position = transform.position;
    }

    private void InvertDirecton()
    {
            _direction.x = -_direction.x;
            _direction.y = -_direction.y;
    }
}
