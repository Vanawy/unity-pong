using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int _LScore = 0;
    private int _RScore = 0;

    [SerializeField]
    private Text scoreText;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag != "Ball") {
            return;
        } 
        if (other.transform.position.x < 0) {
            _RScore++;
        } else {
            _LScore++;
        }
        UpdateScoreText();

        other.GetComponent<Ball>().ResetState();
    }

    public void ResetScore()
    {
        _LScore = _RScore = 0;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText) {
            scoreText.text = _LScore + " : " + _RScore;
        }
    }
}
