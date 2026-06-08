using UnityEngine;

// BUG 1 CORREGIDO: La clase se llamaba "Camera", igual que UnityEngine.Camera.
// Eso causa conflictos internos en Unity. Renombrada a CameraFollow.
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject ball;

    // Offset en X e Y por si la cámara no debe estar centrada exactamente en la bola
    [SerializeField] private Vector2 offset = Vector2.zero;

    private void Start()
    {
        // BUG 2 CORREGIDO: Start() estaba vacío.
        // En el primer frame la cámara estaba donde la dejaste en el editor,
        // no encima de la bola. Con esto hace snap inmediato al iniciar.
        if (ball != null)
            ApplyFollow();
    }

    // BUG 3 CORREGIDO: Cambiado Update() por LateUpdate().
    // LateUpdate se ejecuta DESPUÉS de que todos los objetos se hayan movido,
    // así la cámara siempre sigue la posición final de la bola en ese frame
    // y no hay jitter ni desincronía de un frame.
    private void LateUpdate()
    {
        if (ball != null)
            ApplyFollow();
    }

    private void ApplyFollow()
    {
        transform.position = new Vector3(
            ball.transform.position.x + offset.x,
            ball.transform.position.y + offset.y,
            transform.position.z    // Z fija: la configuras en el editor
        );
    }
}
