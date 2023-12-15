using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private bool isGameFrozen = true;

    // Singleton pattern for easy access
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "GameManager";
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    // Property to get the current game freeze state
    public bool IsGameFrozen
    {
        get { return isGameFrozen; }
    }

    // Method to freeze the game
    public void FreezeGame()
    {
        isGameFrozen = true;
        // Add any additional logic you might need when freezing the game
    }

    // Method to unfreeze the game
    public void UnfreezeGame()
    {
        isGameFrozen = false;
        // Add any additional logic you might need when unfreezing the game
    }

    public void StartGame()
    {
        // Unfreeze the game when the game starts
        UnfreezeGame();
    }

    void Update()
    {
        // Check for input to start the game (for example, X key)
        if (isGameFrozen && Input.GetButtonDown("buttonSouth"))
        {
            StartGame();
        }
    }
}
