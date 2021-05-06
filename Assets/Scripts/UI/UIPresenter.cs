using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPresenter : MonoBehaviour
{
    [SerializeField] private Text _score;
    [SerializeField] private Text _coins;
    [SerializeField] private Text _message;

    private void Message(string mess)
    {
        _message.text = mess;
    }
    private void ChangeScoreValue(int score)
    {
        _score.text = score.ToString();
    }
    private void CountCoinsUpdate(int coins)
    {
        _coins.text = "x" + coins;
    }
    private void Start()
    {
        GameController.Instance.OnMessage += Message;
        GameController.Instance.OnSelectCoin += CountCoinsUpdate;
        GameController.Instance.OnChangeScore += ChangeScoreValue;
    }
    private void OnDisable()
    {
        GameController.Instance.OnMessage -= Message;
        GameController.Instance.OnSelectCoin -= CountCoinsUpdate;
        GameController.Instance.OnChangeScore -= ChangeScoreValue;
    }
}
