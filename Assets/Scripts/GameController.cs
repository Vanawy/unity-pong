using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField]
    private Paddle _leftPaddle;
    [SerializeField]
    private Paddle _rightPaddle;
    private IPaddleController _leftController;
    private IPaddleController _rightController;

    [SerializeField]
    private Ball _ball;

    [Header("UI")]
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private string _menuScenePath;

    [SerializeField]
    private Animator _animator;

    [Header("Settings")]
    [SerializeField]
    private bool _isSlowMotionEnabled = true;
    [SerializeField]
    [Range(0.05f, 1f)]
    private float _slowModeScale = 0.45f;
    
    [SerializeField]
    private int _maxScore = 10;

    private Rigidbody2D _bRb;
    private Rigidbody2D _lpRb;
    private Rigidbody2D _rpRb;

    
    private int _lScore = 0;
    private int _rScore = 0;
    private bool _isGameEnded = false;
    private bool _isPaused = false;
    
    void Start()
    {
        Time.timeScale = 1;
        _leftController = GameParameters.leftController;
        _rightController = GameParameters.rightController;

        _bRb = _ball.GetComponent<Rigidbody2D>();
        _lpRb = _leftPaddle.GetComponent<Rigidbody2D>();
        _rpRb = _rightPaddle.GetComponent<Rigidbody2D>();

        _isGameEnded = _isPaused = false;
        ResetScore();
        _ball.ResetState();
    }

    // Update is called once per frame
    void Update()
    {   
        if (!_isPaused) {
            _leftController.UpdatePaddle(_leftPaddle, _ball, _rightPaddle);
            _rightController.UpdatePaddle(_rightPaddle, _ball, _leftPaddle);
        }

        if (_isSlowMotionEnabled && (_bRb.position.x < _lpRb.position.x || _bRb.position.x > _rpRb.position.x)) {
            Time.timeScale = _slowModeScale;
        }

        if (Input.GetButtonDown("Cancel")) {
            TogglePause();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag != "Ball") {
            return;
        } 
        if (!_isGameEnded && other.transform.position.x < 0) {
            _rScore++;
        } else {
            _lScore++;
        }
        if (!_isGameEnded && _lScore + _rScore >= _maxScore) {
            EndGame();
        }
        UpdateScoreText();
        Time.timeScale = 1f;

        other.GetComponent<Ball>().ResetState();
    }

    public void ResetScore()
    {
        _lScore = _rScore = 0;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (!scoreText) return;
        string text = _lScore + " : " + _rScore;
        if (_isGameEnded) {
            if (_lScore > _rScore) {
                text = $"{GameParameters.leftController.GetName()} WINS!";
            } else if(_lScore < _rScore) {
                text = $"{GameParameters.rightController.GetName()} WINS!";
            } else {
                text = "DRAW";
            }
        }
        scoreText.text = text;
    }

    private void EndGame()
    {
        _isGameEnded = true;
        _animator.SetBool("IsGameEnded", _isGameEnded);
        _leftController = new SmartAIController();
        _rightController = new SmartAIController();
    }

    public void TogglePause()
    {
        if(_isPaused) {
            Time.timeScale = 1;
        } else {
            Time.timeScale = 0;
        }
        _isPaused = !_isPaused;
        _animator.SetBool("IsPaused", _isPaused);
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene(_menuScenePath);
    }
}
