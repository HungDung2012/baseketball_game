using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballHoop : MonoBehaviour
{
    [Header("Hoop Settings")]
    public int pointsPerScore = 2;
    public Vector3 ballResetPosition = new Vector3(0, 1.5f, 0);
    public float resetDelay = 2f;
    
    [Header("Visual Effects")]
    public ParticleSystem scoreEffect;
    public GameObject scoreTriggerZone; // Trigger zone bên dưới rổ
    
    private HashSet<GameObject> ballsInHoop = new HashSet<GameObject>();
    
    void Start()
    {
        // Đảm bảo trigger zone là trigger và có script nhận trigger
        if (scoreTriggerZone != null)
        {
            Collider triggerCollider = scoreTriggerZone.GetComponent<Collider>();
            if (triggerCollider != null)
            {
                triggerCollider.isTrigger = true;
            }
            
            // Tự động thêm HoopTriggerHelper nếu chưa có
            HoopTriggerHelper helper = scoreTriggerZone.GetComponent<HoopTriggerHelper>();
            if (helper == null)
            {
                helper = scoreTriggerZone.AddComponent<HoopTriggerHelper>();
            }
            helper.hoopManager = this;
        }
        else
        {
            Debug.LogError("Chưa gán Score Trigger Zone trong BasketballHoop! Kéo ScoreTrigger GameObject vào slot này!");
        }
    }
    
    // Method công khai được gọi từ HoopTriggerHelper
    public void OnBallEnterHoop(GameObject ball)
    {
        if (!ballsInHoop.Contains(ball))
        {
            ballsInHoop.Add(ball);
            StartCoroutine(ScoreBasket(ball));
        }
    }
    
    private IEnumerator ScoreBasket(GameObject ball)
    {
        Debug.Log("✅ GHI ĐIỂM! +" + pointsPerScore + " points");
        
        // Phát hiệu ứng
        if (scoreEffect != null)
        {
            scoreEffect.Play();
        }
        
        // Thêm điểm
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(pointsPerScore);
            Debug.Log($"💯 Score hiện tại: {ScoreManager.Instance.score}");
        }
        else
        {
            Debug.LogError("❌ KHÔNG TÌM THẤY ScoreManager.Instance! Kiểm tra ScoreManager GameObject trong scene!");
        }
        
        // Phát âm thanh swoosh từ bóng
        BasketballController ballController = ball.GetComponent<BasketballController>();
        if (ballController != null)
        {
            ballController.PlaySwooshSound();
        }
        
        // Đợi trước khi reset bóng
        yield return new WaitForSeconds(resetDelay);
        
        // Reset vị trí bóng
        if (ballController != null)
        {
            ballController.ResetBall(ballResetPosition);
        }
        
        ballsInHoop.Remove(ball);
    }
}

// Helper script được tự động thêm vào ScoreTrigger GameObject
public class HoopTriggerHelper : MonoBehaviour
{
    [HideInInspector]
    public BasketballHoop hoopManager;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"🎯 Trigger phát hiện: {other.gameObject.name}, Tag: {other.tag}");
        
        if (other.CompareTag("Basketball"))
        {
            if (hoopManager != null)
            {
                hoopManager.OnBallEnterHoop(other.gameObject);
            }
            else
            {
                Debug.LogError("❌ HoopTriggerHelper: hoopManager = null!");
            }
        }
        else
        {
            Debug.LogWarning($"⚠️ Object '{other.gameObject.name}' không có tag 'Basketball' (tag hiện tại: '{other.tag}')");
        }
    }
}
