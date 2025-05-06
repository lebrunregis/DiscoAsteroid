using TMPro;
using UnityEngine;

public class UiTextDisplay : MonoBehaviour
{

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text playerHealthText;
    [SerializeField] TMP_Text currentWaveText;


    public void UpdateScoreText(int score)
    {
        playerHealthText.text = $"Score : {score}";
    }

    public void UpdateHealthText(int life)
    {
        playerHealthText.text = $"Health : {life}";
    }

    public void UpdateWaveText(int currentWave)
    {
        currentWaveText.text = $"Wave : {currentWave}";
    }
}
