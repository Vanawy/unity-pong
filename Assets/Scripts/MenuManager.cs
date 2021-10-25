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
    
    private AudioSource _menuSounds;
    [Header("Sounds")]
    [SerializeField]
    private AudioClip _clickSound;

    void Start()
    {
        _menuSounds = GetComponent<AudioSource>();
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button btn in buttons)
        {
            btn.onClick.AddListener(() => ClickSound());
        }
    }

    public void ClickSound()
    {
        _menuSounds.PlayOneShot(_clickSound);
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
