using UnityEngine;
using System.Collections;

public class SmartAIController : IPaddleController
{
    private static Rigidbody2D _ballRb = null;

    private const float BORDER_DISTANCE = 5;
    private float _offset;
    private const float _maxOffset = .85f;
    private bool _isOffsetUpdated = false;

    public void Start() {
        UpdateOffset();
    }

    private void UpdateOffset()
    {
        _offset = Random.Range(-_maxOffset, _maxOffset);
    }

    public void UpdatePaddle(Paddle paddle, Ball ball = null)
    {
        paddle.Move(Paddle.Direction.NONE);

        if(_ballRb == null) {
            _ballRb = ball.GetComponent<Rigidbody2D>();
        }

        bool ballMovingTowardsPaddle = Vector3.Dot(paddle.GetFacingDirection(), _ballRb.velocity) < 0;
        float target = 0;

        if(ballMovingTowardsPaddle) {
            if (!_isOffsetUpdated) {
                UpdateOffset();
                _isOffsetUpdated = true;
            }
            target = PredictPosition(paddle, _ballRb);
        } else {
            _isOffsetUpdated = false;
        }


        if (target > paddle.transform.position.y + _offset) {
            paddle.Move(Paddle.Direction.UP);
        } else if (target < paddle.transform.position.y + _offset) {
            paddle.Move(Paddle.Direction.DOWN);
        }
    }

    private float PredictPosition(Paddle paddle, Rigidbody2D _ballRb)
    {
        var bPos = _ballRb.position;
        var bVel = _ballRb.velocity;
        var pPos = paddle.GetPosition();
        bool isFound = false;


        Vector2 intersection = VectorTools.GetIntersectionPointCoordinates(pPos, pPos + Vector2.up, bPos, bPos + bVel, out isFound);

        if (!isFound) return -1f;

        if (intersection.y > BORDER_DISTANCE) {
            return BORDER_DISTANCE - (intersection.y - BORDER_DISTANCE);
        }
        if (intersection.y < -BORDER_DISTANCE) {
            return -BORDER_DISTANCE - (intersection.y + BORDER_DISTANCE);
        }

        return intersection.y;
    }
}