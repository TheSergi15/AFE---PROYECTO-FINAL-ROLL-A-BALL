using UnityEngine;

public class Spring : MonoBehaviour
{
    public void OnShoot()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2.5f);
        foreach (Collider col in colliders)
        {
            Ball ball = col.GetComponent<Ball>();
            if (ball != null)
            {
                ball.Shoot();
            }
        }
    }
}