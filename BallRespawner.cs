using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRespawner : MonoBehaviour
{
    [Header("Respawn Settings")]
    public GameObject ballPrefab;
    public Transform respawnPoint;
    public float respawnHeight = -5f; // Độ cao để respawn bóng nếu rơi xuống
    
    [Header("Multiple Balls")]
    public bool allowMultipleBalls = false;
    public int maxBalls = 3;
    
    private List<GameObject> activeBalls = new List<GameObject>();
    
    void Start()
    {
        if (respawnPoint == null)
        {
            respawnPoint = transform;
        }
        
        // Spawn bóng đầu tiên
        SpawnBall();
       
    }
    
    void Update()
    {
        // Kiểm tra các bóng có rơi xuống dưới không
        for (int i = activeBalls.Count - 1; i >= 0; i--)
        {
            if (activeBalls[i] == null)
            {
                activeBalls.RemoveAt(i);
            }
            else if (activeBalls[i].transform.position.y < respawnHeight)
            {
                RespawnBall(activeBalls[i]);
            }
        }
    }
    
    public void SpawnBall()
    {
        if (!allowMultipleBalls && activeBalls.Count > 0)
        {
            return;
        }
        
        if (activeBalls.Count >= maxBalls)
        {
            return;
        }
        
        GameObject newBall = Instantiate(ballPrefab, respawnPoint.position, Quaternion.identity);
        activeBalls.Add(newBall);
    }
    
    private void RespawnBall(GameObject ball)
    {
        BasketballController controller = ball.GetComponent<BasketballController>();
        if (controller != null)
        {
            controller.ResetBall(respawnPoint.position);
        }
        else
        {
            ball.transform.position = respawnPoint.position;
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
