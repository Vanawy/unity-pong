using UnityEngine;
using System.Collections;

public class PlayerController : IPaddleController
{
    public void UpdatePaddle(Paddle paddle, Ball ball = null)
    {
        Paddle.Direction direction = Paddle.Direction.NONE;
        if (Input.GetKey(KeyCode.UpArrow)) {
            direction = Paddle.Direction.UP;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            direction = Paddle.Direction.DOWN;
        }
        paddle.Move(direction);
    }
}