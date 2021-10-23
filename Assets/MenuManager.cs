using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private string _gameScenePath;
    

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(_gameScenePath);
    }

    public void Options()
    {

    }

    public void ExitGame()
    {

    }
}
