using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField]
    private Paddle leftPaddle;
    private IPaddleController leftController;
    
    [SerializeField]
    private Paddle rightPaddle;
    private IPaddleController rightController;

    [SerializeField]
    private Ball ball;
    
    void Start()
    {
        leftController = GameParameters.leftController;
        rightController = GameParameters.rightController;
    }

    // Update is called once per frame
    void Update()
    {
        leftController.UpdatePaddle(leftPaddle, ball);
        rightController.UpdatePaddle(rightPaddle, ball);
    }
}
