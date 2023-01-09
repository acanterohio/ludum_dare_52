using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI high;

    void Start()
    {
        score.text = "Score: $" + Mathf.Round(PlayerPrefs.GetFloat("Score") * 100f) / 100f;
        high.text = "High Score: $" + Mathf.Round(PlayerPrefs.GetFloat("HighScore") * 100f) / 100f;
    }
}
