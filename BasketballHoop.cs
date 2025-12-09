using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballHoop : MonoBehaviour
{
    [Header("Hoop Settings")]
    public int pointsPerScore = 1;
    public Vector3 ballResetPosition = new Vector3(0, 1.5f, 0);
    public float resetDelay = 0.01f;
    
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
        // Phát hiệu ứng khi bóng vào rổ
        if (scoreEffect != null)
        {
            scoreEffect.Play();
        }
        
        // Cập nhật điểm khi ném bóng vào rổ
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(pointsPerScore);
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
        if (other.CompareTag("Basketball"))
        {
            if (hoopManager != null)
            {
                hoopManager.OnBallEnterHoop(other.gameObject);
            }
        }
    }
}
