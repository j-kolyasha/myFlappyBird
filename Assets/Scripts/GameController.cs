using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public event UnityAction<string> OnMessage;
    public event UnityAction<int> OnSelectCoin;
    public event UnityAction<int> OnChangeScore;

    private int _score = 0;
    private int _coins = 0;

    public static GameController Instance { get; private set; }
    public GameState GameCondition { get; private set; }
    public int Score => _score;

    public void SelectCoin()
    {
        _coins += 1;
        OnSelectCoin?.Invoke(_coins);
    }
    public void ChangeScore()
    {
        _score += 1;
        OnChangeScore?.Invoke(_score);
    }
    public void Loss()
    {
        OnMessage?.Invoke("Loss");
        SetGameCondition(GameState.Loss);
    }
    private void SetGameCondition(GameState state)
    {
        GameCondition = state;
    }
    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.1f);
        OnMessage?.Invoke("3");
        yield return new WaitForSeconds(0.25f);
        OnMessage?.Invoke("2");
        yield return new WaitForSeconds(0.25f);
        OnMessage?.Invoke("1");
        yield return new WaitForSeconds(0.25f);
        OnMessage?.Invoke("Start");
        yield return new WaitForSeconds(0.25f);
        OnMessage?.Invoke("");

        SetGameCondition(GameState.Game);
    }
    private void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameCondition == GameState.Game)
            {
                OnMessage?.Invoke("Pause");
                SetGameCondition(GameState.Pause);
            }
            else if (GameCondition == GameState.Pause)
            {
                OnMessage?.Invoke("");
                SetGameCondition(GameState.Game);
            }
        }
        if (GameCondition == GameState.Loss && Input.GetKeyDown(KeyCode.Space))
        {
            NewGame();
        }
    }
    private void Awake()
    {
        Instance = Instance == null ? this : Instance;

        StartCoroutine(nameof(StartGame));
    }
    
    public enum GameState
    {
        Pause,
        Game,
        Loss,
    }
}
