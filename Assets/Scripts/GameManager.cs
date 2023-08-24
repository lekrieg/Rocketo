// ////////////////////////
// File: GameManager.cs
// Created at: 08 22, 2023
// Description:
// 
// Modified by: Daniel Henrique
// 08 24, 2023
// ////////////////////////

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // private float _distance;
    public static GameManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;

        // _distance = 0;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene($"{sceneName}Scene");
    }
}