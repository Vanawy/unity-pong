using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PaddleControllerSelector : MonoBehaviour {

    [SerializeField]
    private int _selectedControllerIndex = 0;

    private IPaddleController[] _controllers = {
        new PlayerController(),
        new EasyAIController(),
        new NormalAIController(),
        new SmartAIController(),
    };

    [Header("Sprites")]
    [SerializeField]
    private Sprite[] _sprites;

    [SerializeField]
    private Text _typeText;
    [SerializeField]
    private Image _typeImage;

    public void Start() {
        UpdateUI();
    }

    public IPaddleController GetController() {
        return _controllers[_selectedControllerIndex];
    }

    private void UpdateUI()
    {
        IPaddleController controller = GetController();
        _typeText.text = controller.GetName();
        _typeImage.sprite = _sprites[_selectedControllerIndex];
    }
    
    public void NextType()
    {
        _selectedControllerIndex++;
        if (_selectedControllerIndex >= _controllers.Length) {
            _selectedControllerIndex = 0;
        }
        UpdateUI();
    }
}