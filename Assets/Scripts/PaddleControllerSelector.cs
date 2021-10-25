using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PaddleControllerSelector : MonoBehaviour {
    public enum ControllerType {
        Player,
        EasyAi,
        NormalAi,
        SmartAi,
    }

    [SerializeField]
    private Text _typeText;

    public void Start() {
        UpdateUI();
    }

    public static IPaddleController GetPaddleController(ControllerType type) {
        switch (type)
        {
            case ControllerType.Player:
                return new PlayerController();
            case ControllerType.EasyAi:
                return new EasyAIController();
            case ControllerType.NormalAi:
                return new NormalAIController();
            case ControllerType.SmartAi:
                return new SmartAIController();
            default:
                throw new System.Exception("Unknown paddle controller type");
        }
    }

    [SerializeField]
    private ControllerType _type;

    public IPaddleController GetCurrentController()
    {
        return GetPaddleController(_type);
    }

    private string GetText()
    {
        switch (_type)
        {
            case ControllerType.Player:
                return "Human";
            case ControllerType.EasyAi:
                return "Easy AI";
            case ControllerType.NormalAi:
                return "Normal AI";
            case ControllerType.SmartAi:
                return "Smart AI";
            default:
                return "";
        }
    }

    private void UpdateUI()
    {
        _typeText.text = GetText();
    }

    public void SetType(ControllerType type)
    {
        _type = type;
        UpdateUI();
    }

    public void NextType()
    {
        _type++;
        if (!Enum.IsDefined(typeof(ControllerType), _type)) {
            _type = ControllerType.Player;
        }
        UpdateUI();
    }
}