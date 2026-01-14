using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f; // 공격 투사체 속도
    public float damage = 1f; // 공격 데미지

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 1초 후 게임 오브젝트 삭제
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }
}