using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { 
    Menu, 
    inGame, 
    GameOver 
};


public class GameManager : MonoBehaviour {

    public GameState currentGameState = GameState.Menu;

    public static GameManager sharedInstance;

    private PlayerController playerController;

    private void Awake() {
        if (sharedInstance == null) {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start() {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {  
            StartGame();
        }
    }
    public void StartGame() {
        SetGameState(GameState.inGame);
    }

    public void GameOver() {
        SetGameState(GameState.GameOver);
    }

    public void BackToMenu() {
        SetGameState(GameState.Menu);
    }

    private void SetGameState(GameState newGameState) {
        /* if (newGameState == GameState.Menu) {
            // Show menu
        } else if (newGameState == GameState.inGame) {
            // Start game
        } else if (newGameState == GameState.GameOver) {
            // Show game over
        } */ 

        switch (newGameState) {
            case GameState.Menu:
                // Show menu
                break;
            case GameState.inGame:
                playerController.StartGame();
                // Start game
                break;
            case GameState.GameOver:
                // Show game over
                break;
        }
        this.currentGameState = newGameState;
    }
}
