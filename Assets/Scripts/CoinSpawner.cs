using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private PipeController _pipeController;
    [SerializeField, Range(0, 1f)] private float _chanceSpawn;
    [SerializeField] private AnimationCurve _speed;
    [SerializeField] private float _maxHeightPosition, _minHeightPosition;
    [SerializeField] private float _spawnCooldownTime;
    [SerializeField] private Vector2 _spawnPosition;

    private Coin _currentCoin;
    private float _spawnTime;

    private void Update()
    {
        if (GameController.Instance.GameCondition != GameController.GameState.Game) return;

        if (_spawnTime <= 0)
        {
            if ((_pipeController.SpawnTime <= _pipeController.SpawnCooldownTime / 2) && (Random.Range(0, 1f) <= _chanceSpawn))
            {
                if (_currentCoin != null)
                    Destroy(_currentCoin.gameObject);

                _currentCoin = Instantiate(_coin, new Vector2(_spawnPosition.x, Random.Range(_minHeightPosition, _maxHeightPosition)), Quaternion.identity, transform);
                _spawnTime = _spawnCooldownTime;    
            }
        }

        if (_currentCoin != null)
        {
            Vector3 position = _currentCoin.transform.position;
            position.x -= Time.deltaTime * _speed.Evaluate(GameController.Instance.Score);
            _currentCoin.transform.position = position;
        }

        _spawnTime -= Time.deltaTime;
    }
}
