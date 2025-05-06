using TMPro;
using UnityEngine;

public class UiDisplayText : MonoBehaviour
{

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text playerHealthText;
    [SerializeField] TMP_Text currentWaveText;


    public void UpdateHealthText(int life)
    {
        playerHealthText.text = $"Health : {life}";
    }

    public void UpdateWaveText(int currentWave)
    {
        currentWaveText.text = $"Wave : {currentWave}";
    }
}
