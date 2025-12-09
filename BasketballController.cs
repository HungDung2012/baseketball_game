using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BasketballController : MonoBehaviour
{
    [Header("Basketball Settings")]
    public float throwForceMultiplier = 1.5f;
    public AudioClip bounceSound;
    public AudioClip swooshSound;
    
    private Rigidbody rb;
    private AudioSource audioSource;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private Vector3 lastPosition;
    private Vector3 velocity;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        
        // Cấu hình Rigidbody cho bóng rổ
        rb.mass = 0.6f; // Khối lượng bóng rổ thực (kg)
        rb.drag = 0.1f;
        rb.angularDrag = 0.05f;
        rb.useGravity = true;
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        // Setup XR Grab Interactable nếu chưa có
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable == null)
        {
            grabInteractable = gameObject.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        }
        
        // Đăng ký event khi thả bóng
        grabInteractable.selectExited.AddListener(OnRelease);
        
        lastPosition = transform.position;
    }
    
    void Update()
    {
        // Tính vận tốc để ném bóng chính xác hơn
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }
    
    private void OnRelease(SelectExitEventArgs args)
    {
        // Áp dụng lực ném khi người chơi thả bóng
        if (rb != null)
        {
            rb.velocity = velocity * throwForceMultiplier;
            
            // Thêm spin cho bóng
            Vector3 angularVelocity = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            ) * 2f;
            rb.angularVelocity = angularVelocity;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        // Phát âm thanh khi bóng va chạm
        if (collision.relativeVelocity.magnitude > 1f && bounceSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(bounceSound, Mathf.Clamp01(collision.relativeVelocity.magnitude / 10f));
        }
    }
    
    public void PlaySwooshSound()
    {
        if (swooshSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(swooshSound);
        }
    }
    
    public void ResetBall(Vector3 resetPosition)
    {
        transform.position = resetPosition;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
