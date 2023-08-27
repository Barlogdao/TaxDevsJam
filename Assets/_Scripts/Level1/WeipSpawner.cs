
using System.Collections;
using UnityEngine;

public class WeipSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private WeipMover _prefab;
    [SerializeField] float _interval = 4f;
    [SerializeField] Level1Timer _timer;

    private void OnEnable()
    {
        Level1Logic.GameStarted += OnGameStarted;
        _timer.Elapsed += OnTimerElapced;

    }

    private void OnTimerElapced()
    {
        _timer.Elapsed -= OnTimerElapced;
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        Level1Logic.GameStarted -= OnGameStarted;
    }

    private void OnGameStarted()
    {
        StartCoroutine(SpawningWeips());
    }

    private IEnumerator SpawningWeips()
    {
        while (true)
        {
            Instantiate(_prefab, _points[UnityEngine.Random.Range(0, _points.Length - 1)].position, Quaternion.identity).Init(_timer);
            yield return new WaitForSeconds(_interval);
        }
    }
}
