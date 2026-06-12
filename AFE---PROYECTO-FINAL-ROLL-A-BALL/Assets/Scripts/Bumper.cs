using UnityEngine;

public class Bumper : MonoBehaviour
{
    [Header("Rebote")]
    [SerializeField] private float bumpForce = 12f;
    [SerializeField] private float cooldown = 0.15f;

    [Header("Efectos (opcional)")]
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitSound;

    private float lastHitTime = -999f;

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time - lastHitTime < cooldown) return;

        Ball ball = collision.collider.GetComponent<Ball>();
        if (ball == null) return;

        Rigidbody ballRb = collision.rigidbody;
        if (ballRb == null) return;

        lastHitTime = Time.time;

        // Dirección desde el bumper hacia la bola, proyectada sobre el plano de la mesa
        Vector3 direction = ballRb.position - transform.position;
        direction -= Vector3.Project(direction, transform.up);
        direction.Normalize();

        ballRb.AddForce(direction * bumpForce, ForceMode.Impulse);

        if (animator != null)
            animator.SetTrigger("Hit");

        if (audioSource != null && hitSound != null)
            audioSource.PlayOneShot(hitSound);
    }
}