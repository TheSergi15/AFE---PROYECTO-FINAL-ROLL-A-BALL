using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] GameObject ball;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(ball.transform.position.x, ball.transform.position.y, transform.position.z);
    }
}
