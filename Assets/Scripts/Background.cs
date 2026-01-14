using UnityEngine;
using UnityEngine.UIElements;

public class Background : MonoBehaviour
{ 
    
    private float moveSpeed = 3f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        
        if (transform.position.y < -10)
        {
            transform.position = new Vector3(0, 10f, 0);
        }
    }
}
