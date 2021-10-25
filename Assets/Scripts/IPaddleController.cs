using UnityEngine;
using System.Collections;

public interface IPaddleController {

      void UpdatePaddle(Paddle paddle, Ball ball = null);
}