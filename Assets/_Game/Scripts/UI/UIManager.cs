using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score;

    [SerializeField] private TextMeshProUGUI highScoreText;
    private int highScore;

    [SerializeField] private TextMeshProUGUI coinsText;

    [SerializeField] private TextMeshProUGUI waveText;
    private int wave;

    [SerializeField] private Image[] lifesSprites;
    [SerializeField] private Image healthBar;

    [SerializeField] private Sprite[] healthBars;

    private Color32 active = new Color(1, 1, 1, 1);
    private Color32 inactive = new Color(1, 1, 1, 0.25f);

    private static UIManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public static int GetHighScore() => instance.highScore;
    

    public static void UpdateLives(int value)
    {
        foreach (Image item in instance.lifesSprites)
        {
            item.color = instance.inactive;
        }

        for (int i = 0; i < value; i++)
        {
            instance.lifesSprites[i].color = instance.active;
        }
    }

    public static void UpdateHealthBar(int value)
    {
        instance.healthBar.sprite = instance.healthBars[value];
    }

    public static void UpdateScore(int value)
    {
        instance.score += value;
        instance.scoreText.text = instance.score.ToString().PadLeft(3, '0');
    }

    public static void UpdateHighScore(int highScore)
    {
        if (instance.highScore < highScore)
        {
            instance.highScore = highScore;
            instance.highScoreText.text = instance.highScore.ToString().PadLeft(3, '0');
        }
    }

    public static void UpdateWave()
    {
        instance.wave++;
        instance.waveText.text = instance.wave.ToString().PadLeft(3, '0');
    }

    public static void UpdateCoins()
    {
        instance.coinsText.text = Inventory.CurrentCoins.ToString().PadLeft(3, '0');
    }

    public static void ResetUI()
    {
        instance.score = 0;
        instance.wave = 0;
        instance.scoreText.text = instance.score.ToString().PadLeft(3, '0');
        instance.waveText.text = instance.wave.ToString().PadLeft(3, '0');
    }
}
