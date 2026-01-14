using UnityEngine;

public class Coin : MonoBehaviour
{
    private float minY = -7f; // 코인이 아래로 사라졌을때 어디서 사라질지
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Jump();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < minY)
        {
            Destroy(gameObject);
        }
    }

    void Jump()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        float randomJumpForce = Random.Range(4f, 8f);
        Vector2 jumpVelocity = Vector2.up * randomJumpForce;
        jumpVelocity.x = Random.Range(-2f, 2f);
        
        
        rigidbody.AddForce(jumpVelocity,  ForceMode2D.Impulse);
    }
}
