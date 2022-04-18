using System;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState obj)
    {
        throw new NotImplementedException();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}