using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject coin;
    
    [SerializeField] private float moveSpeed = 10f; // 적 이동속도

    private float minY = -7f; // 적이 아래로 사라졌을때 어디서 사라질지

    [SerializeField] private float hp = 1f; // 적 체력


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        // 해당 적이 특정 y값에 오면 게임 오브젝트 삭제 처리
        if (transform.position.y < minY)
        {
            Destroy(gameObject);
        }
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    // 충돌 했을때 처리 되는 메소드 (isTrigger가 켜져있을때)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Weapon")
        {
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            hp -= weapon.damage;

            
            if (hp <= 0)
            {
                if (gameObject.tag == "Boss")
                {
                    GameManager.instance.SetGameOver();   
                }
                
                // 적의 체력이 0보다 낮으면 적 삭제
                Destroy(gameObject);
                Instantiate(coin, transform.position, Quaternion.identity);
                
                
            }

            // 적과 미사일이 충돌시 미사일 삭제
            Destroy(other.gameObject);
        }
    }
}