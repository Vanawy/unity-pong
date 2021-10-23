using UnityEngine;
using System.Collections;

public class EasyAIController : IPaddleController
{
    private static Rigidbody2D _ballRb = null;
    
    public float _threshold = .9f;

    public void UpdatePaddle(Paddle paddle, Ball ball = null)
    {
        paddle.Move(Paddle.Direction.NONE);

        if(_ballRb == null) {
            _ballRb = ball.GetComponent<Rigidbody2D>();
        }

        bool ballMovingTowardsPaddle = Vector3.Dot(paddle.GetFacingDirection(), _ballRb.velocity) < 0;
        if(!ballMovingTowardsPaddle) {
            return;
        }

        float threshold = _threshold * (1 - ball.GetNormalizedSpeed());

        if (ball.transform.position.y > paddle.transform.position.y + threshold) {
            paddle.Move(Paddle.Direction.UP);
        } else if (ball.transform.position.y < paddle.transform.position.y - threshold) {
            paddle.Move(Paddle.Direction.DOWN);
        }
    }
}