using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject[] _startBackground;
    [SerializeField] private Vector2 _spawnPosition;
    [SerializeField] private Vector2 _endPosition;
    [SerializeField] private AnimationCurve _speed;
 
    private List<GameObject> _currentBackgrounds;


    private void Update()
    {
        if (GameController.Instance.GameCondition != GameController.GameState.Game) return;

        if (_currentBackgrounds.Count > 3)
            DestroyBacground();

        for (int i = 0; i < _currentBackgrounds.Count; i++)
        {
            Transform background = _currentBackgrounds[i].transform;
            Vector3 newPosition = background.position;
            newPosition.x -= Time.deltaTime * _speed.Evaluate(GameController.Instance.Score);
            background.position = newPosition;
        }

        if (_currentBackgrounds[_currentBackgrounds.Count-1].transform.position.x <= _endPosition.x)
            SpawnBackground();
    }
    private void DestroyBacground()
    {
        Destroy(_currentBackgrounds[0]);
        _currentBackgrounds.RemoveAt(0);
    }
    private void SpawnBackground()
    {
        GameObject background = Instantiate(_background, _spawnPosition, Quaternion.identity, transform);
        _currentBackgrounds.Add(background);
    }
    private void Start()
    {
        _currentBackgrounds = new List<GameObject>();

        for (int i = 0; i < _startBackground.Length; i++)
        {
            _currentBackgrounds.Add(_startBackground[i]);
        }
    }
}
