using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MainMenu, GamePlay, Finish, Revive, Setting }

public class GameManager : Singleton<GameManager>
{
    private static GameState gameState;
    [SerializeField] GameObject winGameCanvas;

    public static void ChangeState(GameState state)
    {
        gameState = state;
    }

    public static bool IsState(GameState state) => gameState == state;

    private void Awake()
    {
        //tranh viec nguoi choi cham da diem vao man hinh
        Input.multiTouchEnabled = false;
        //target frame rate ve 60 fps
        Application.targetFrameRate = 60;
        //tranh viec tat man hinh
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //xu tai tho
        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.StartListening("WinGame", WinGame);
    }
    private void OnDisable()
    {
        if (!EventManager.CheckNull())
            EventManager.Instance.StopListening("WinGame", WinGame);
    }

    private void WinGame(object[] parameters)
    {
        StartCoroutine(WinGame());
    }

    IEnumerator WinGame()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(1f);

        // Then activate the win game canvas
        winGameCanvas.SetActive(true);
    }
    private void Start()
    {
        MapGenerator.Instance.StartGenerate();
        //UIManager.Ins.OpenUI<UIMainMenu>();
    }
}