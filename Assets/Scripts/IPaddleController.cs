using UnityEngine;
using System.Collections;

public interface IPaddleController {
      string GetName();
      void UpdatePaddle(Paddle paddle, Ball ball, Paddle enemy);
}