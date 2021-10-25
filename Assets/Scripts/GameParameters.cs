public static class GameParameters
{
    public static IPaddleController leftController = new NormalAIController();
    public static IPaddleController rightController = new EasyAIController();
}
