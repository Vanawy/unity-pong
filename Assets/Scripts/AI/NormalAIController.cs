using UnityEngine;
using System.Collections;

public class NormalAIController : IPaddleController
{
    private const string NAME = "Normal AI"; 
    private static Rigidbody2D _ballRb = null;


    
    private float _threshold = .1f;
    private float _offset;
    private const float _maxOffset = .5f;
    private bool _isOffsetUpdated = false;

    public void Start() {
        UpdateOffset();
    }

    private void UpdateOffset()
    {
        _offset = Random.Range(-_maxOffset, _maxOffset);
    }

    public void UpdatePaddle(Paddle paddle, Ball ball, Paddle enemy)
    {
        paddle.Move(Paddle.Direction.NONE);

        if(_ballRb == null) {
            _ballRb = ball.GetComponent<Rigidbody2D>();
        }

        bool ballMovingTowardsPaddle = Vector3.Dot(paddle.GetFacingDirection(), _ballRb.velocity) < 0;
        if(!ballMovingTowardsPaddle) {
            if (_isOffsetUpdated) return;
            UpdateOffset();
            _isOffsetUpdated = true;
            return;
        }
        _isOffsetUpdated = false;

        float threshold = _threshold * (1 - ball.GetNormalizedSpeed());

        if (ball.transform.position.y > paddle.transform.position.y + _offset + threshold) {
            paddle.Move(Paddle.Direction.UP);
        } else if (ball.transform.position.y < paddle.transform.position.y + _offset - threshold) {
            paddle.Move(Paddle.Direction.DOWN);
        }
    }

    public string GetName()
    {
        return NAME;
    }
}