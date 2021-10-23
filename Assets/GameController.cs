using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField]
    private Paddle leftPaddle;

    [SerializeField]
    private PaddleControllerSelector.ControllerType leftControllerType;
    private IPaddleController leftController;
    
    [SerializeField]
    private Paddle rightPaddle;

    [SerializeField]
    private PaddleControllerSelector.ControllerType rightControllerType;
    private IPaddleController rightController;

    [SerializeField]
    private Ball ball;
    
    void Start()
    {
        leftController = PaddleControllerSelector.GetPaddleController(leftControllerType);
        rightController = PaddleControllerSelector.GetPaddleController(rightControllerType);
    }

    // Update is called once per frame
    void Update()
    {
        leftController.UpdatePaddle(leftPaddle, ball);
        rightController.UpdatePaddle(rightPaddle, ball);
    }
}
