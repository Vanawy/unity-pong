using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private string _gameScenePath;

    [SerializeField]
    private PaddleControllerSelector _left;  

    [SerializeField]
    private PaddleControllerSelector _right;    

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartGame()
    {
        GameParameters.leftController  = _left.GetCurrentController();
        GameParameters.rightController = _right.GetCurrentController();
        SceneManager.LoadScene(_gameScenePath);
    }

    public void Options()
    {

    }

    public void ExitGame()
    {

    }
}
