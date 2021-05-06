using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    [SerializeField] private Chank _chank;
    [SerializeField] private AnimationCurve _speed;
    [SerializeField] private int _maxCountChanks;
    [SerializeField] private float _maxHeightPosition, _minHeightPosition;
    [SerializeField] private Vector2 _spawnPosition;
    [SerializeField] private float _spawnCooldownTime;
    
    private float _spawnTime;
    private List<Chank> _currentChanks;

    public float SpawnTime => _spawnTime;
    public float SpawnCooldownTime => _spawnCooldownTime;
    
    private void Update()
    {
        if (GameController.Instance.GameCondition != GameController.GameState.Game) return;

        if (_currentChanks.Count > _maxCountChanks)
        {
            DestroyChank();
        }

        for (int i = 0; i < _currentChanks.Count; i++)
        {
            Transform chank = _currentChanks[i].transform;
            Vector2 newPosition = chank.position;
            newPosition.x -= Time.deltaTime * _speed.Evaluate(GameController.Instance.Score);
            chank.position = newPosition;
        }

        if (_spawnTime <= 0)
        {
            SpawnChank();
        }
        _spawnTime -= Time.deltaTime;
    }

    private void DestroyChank()
    {
        Destroy(_currentChanks[0].gameObject);
        _currentChanks.RemoveAt(0);
    }
    private void SpawnChank()
    {
        float y = Random.Range(_minHeightPosition, _maxHeightPosition);
        Chank chank = Instantiate(_chank, new Vector2(_spawnPosition.x, y), Quaternion.identity, transform);
        _currentChanks.Add(chank);

        _spawnTime = _spawnCooldownTime - _speed.Evaluate(GameController.Instance.Score) * Time.deltaTime;
    }
    private void Start()
    {
        _currentChanks = new List<Chank>();
    }
}
