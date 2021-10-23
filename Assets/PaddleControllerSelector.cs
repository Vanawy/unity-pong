using UnityEngine;

public static class PaddleControllerSelector {
    public enum ControllerType {
        Player,
        EasyAi,
    }

    public static IPaddleController GetPaddleController(ControllerType type) {
        switch (type)
        {
            case ControllerType.Player:
                return new PlayerController();
            case ControllerType.EasyAi:
                return new EasyAIController();
            default:
                throw new System.Exception("Unknown paddle controller type");
        }
    }
}