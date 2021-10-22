using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Start Button")]
    [SerializeField]
    private string _gameScenePath;
    [SerializeField]
    private Button _startButton;
    

    void Start()
    {
        _startButton.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void StartGame()
    {
        SceneManager.LoadScene(_gameScenePath);
    }
}
