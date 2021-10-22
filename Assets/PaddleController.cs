using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static App.RandomTools;

public class PaddleController : MonoBehaviour
{
    // Start is called before the first frame update
    

    [SerializeField]
    private Paddle leftPaddle;
    
    [SerializeField]
    private Paddle rightPaddle;

    [SerializeField]
    private Ball ball;

    private Rigidbody2D ballRb;
    
    void Start()
    {
        ballRb = ball.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerPaddle(leftPaddle);
        // UpdatePlayerPaddle(rightPaddle);
        UpdateAIPaddle(rightPaddle);
    }
    
    void UpdatePlayerPaddle(Paddle paddle)
    {
        if (!paddle) {
            return;
        }
        Paddle.Direction direction = Paddle.Direction.NONE;
        if (Input.GetKey(KeyCode.UpArrow)) {
            direction = Paddle.Direction.UP;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            direction = Paddle.Direction.DOWN;
        }
        paddle.Move(direction);
    }

    void UpdateAIPaddle(Paddle paddle)
    {
        if (!paddle) {
            return;
        }
        paddle.Move(Paddle.Direction.NONE);

        bool ballMovingTowardsPaddle = Vector3.Dot(leftPaddle.GetFacingDirection(), ballRb.velocity) > 0;
        if(!ballMovingTowardsPaddle) {
            return;
        }

        if (ball.transform.position.y > paddle.transform.position.y) {
            paddle.Move(Paddle.Direction.UP);
        } else {
            paddle.Move(Paddle.Direction.DOWN);
        }
    }
}
