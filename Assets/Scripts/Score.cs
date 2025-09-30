using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance { get; private set; }
    public TextMeshProUGUI[] scoreText;
    public TextMeshProUGUI[] HighScoreText;
    public int score { get; private set; }
    public int HighScore 
    {  
        get => PlayerPrefs.GetInt("HighScore", 0); 
        private set => PlayerPrefs.SetInt("HighScore", value);
    }  
    private int loadedHighScore;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        loadedHighScore = HighScore;
        foreach (var text in HighScoreText)
        {
            text.text = $"{loadedHighScore}";
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        foreach (var text in scoreText)
        {
            text.text = $"{score}";
        }
        if (score > loadedHighScore)
        {
            HighScore = score;
        }
    }
}
