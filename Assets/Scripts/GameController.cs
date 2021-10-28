using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField]
    private Paddle _leftPaddle;
    private IPaddleController _leftController;
    
    [SerializeField]
    private Paddle _rightPaddle;
    private IPaddleController _rightController;

    [SerializeField]
    private Ball _ball;

    [SerializeField]
    private Text scoreText;

    [Header("Settings")]
    [SerializeField]
    private bool _isSlowMotionEnabled = true;
    [SerializeField]
    [Range(0.05f, 1f)]
    private float _slowModeScale = 0.25f;
    
    [SerializeField]
    private int _maxScore = 10;

    private Rigidbody2D _bRb;
    private Rigidbody2D _lpRb;
    private Rigidbody2D _rpRb;

    
    private int _LScore = 0;
    private int _RScore = 0;
    
    void Start()
    {
        _leftController = GameParameters.leftController;
        _rightController = GameParameters.rightController;

        _bRb = _ball.GetComponent<Rigidbody2D>();
        _lpRb = _leftPaddle.GetComponent<Rigidbody2D>();
        _rpRb = _rightPaddle.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _leftController.UpdatePaddle(_leftPaddle, _ball, _rightPaddle);
        _rightController.UpdatePaddle(_rightPaddle, _ball, _leftPaddle);

        if (_isSlowMotionEnabled && (_bRb.position.x < _lpRb.position.x || _bRb.position.x > _rpRb.position.x)) {
            Time.timeScale = _slowModeScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag != "Ball") {
            return;
        } 
        if (other.transform.position.x < 0) {
            _RScore++;
        } else {
            _LScore++;
        }
        UpdateScoreText();
        Time.timeScale = 1f;

        other.GetComponent<Ball>().ResetState();
    }

    public void ResetScore()
    {
        _LScore = _RScore = 0;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (!scoreText) return;
        string text = _LScore + " : " + _RScore;
        if (_LScore + _RScore >= _maxScore) {
            if (_LScore > _RScore) {
                text = $"{_leftController.GetName()} WINS!";
            } else if(_LScore < _RScore) {
                text = $"{_rightController.GetName()} WINS!";
            } else {
                text = "DRAW";
            }
        }
        scoreText.text = text;
    }
}
