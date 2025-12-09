using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI timerText;
    
    [Header("Game Settings")]
    public bool useTimer = false;
    public float gameDuration = 60f; // 60 giây
    
    private float timeRemaining;
    private bool gameActive = false;
    
    void Start()
    {
        if (useTimer)
        {
            timeRemaining = gameDuration;
            gameActive = true;
        }
        
        // Ẩn combo text ban đầu
        if (comboText != null)
        {
            comboText.gameObject.SetActive(false);
        }
    }
    
    void Update()
    {
        if (useTimer && gameActive)
        {
            timeRemaining -= Time.deltaTime;
            
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                gameActive = false;
                EndGame();
            }
            
            UpdateTimerUI();
        }
    }
    
    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
    
    private void EndGame()
    {

    }
    
    public void RestartGame()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }
        
        if (useTimer)
        {
            timeRemaining = gameDuration;
            gameActive = true;
        }
    }
}
