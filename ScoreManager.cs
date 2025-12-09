using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    
    [Header("Score Settings")]
    public int score = 0;
    public int highScore = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public AudioClip scoreSound;
    
    [Header("Combo System")]
    public int currentCombo = 0;
    public float comboResetTime = 5f;
    public TextMeshProUGUI comboText;
    
    private AudioSource audioSource;
    private float lastScoreTime;
    
    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        // Load high score từ PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        
        Debug.Log($"📊 ScoreManager initialized. High Score: {highScore}");
        
        UpdateUI();
    }
    
    void Update()
    {
        // Reset combo nếu quá lâu không ghi điểm
        if (currentCombo > 0 && Time.time - lastScoreTime > comboResetTime)
        {
            ResetCombo();
        }
    }
    
    public void AddScore(int points)
    {
        // Tăng combo
        currentCombo++;
        lastScoreTime = Time.time;
        
        // Tính điểm với combo multiplier
        int comboBonus = currentCombo > 1 ? (currentCombo - 1) : 0;
        int totalPoints = points + comboBonus;
        
        score += totalPoints;
        
        // Cập nhật high score
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
        
        // Phát âm thanh
        if (scoreSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(scoreSound);
        }
        
        UpdateUI();
    }
    
    private void ResetCombo()
    {
        currentCombo = 0;
        UpdateUI();
    }
    
    public void ResetScore()
    {
        score = 0;
        currentCombo = 0;
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
        
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore.ToString();
        }
        
        if (comboText != null)
        {
            if (currentCombo > 1)
            {
                comboText.text = "Combo x" + currentCombo.ToString();
                comboText.gameObject.SetActive(true);
            }
            else
            {
                comboText.gameObject.SetActive(false);
            }
        }
    }
}
