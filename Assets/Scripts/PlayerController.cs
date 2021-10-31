using UnityEngine;
using System.Collections;

public class PlayerController : IPaddleController
{
    private const string NAME = "Player";

    public string GetName()
    {
        return NAME;
    }

    public void UpdatePaddle(Paddle paddle, Ball ball, Paddle enemy)
    {
        Paddle.Direction direction = Paddle.Direction.NONE;
        if (Input.GetAxisRaw("Vertical") > 0) {
            direction = Paddle.Direction.UP;
        } else if(Input.GetAxisRaw("Vertical") < 0) {
            direction = Paddle.Direction.DOWN;
        }
        paddle.Move(direction);
    }
}