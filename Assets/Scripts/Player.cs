using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed; // 이동속도

    [SerializeField] private GameObject[] weapons; // 무기
    private int weaponIndex = 0; // 무기 레벨
    public Sprite currentWeaponSprite;

    [SerializeField] private Transform shootTransform; // 공격 위치

    [SerializeField] private float shootInterval = 0.05f; // 공격 딜레이
    private float lastShotTime = 0f;

    Rigidbody2D rb;
    Vector2 moveTo;


    public void Run()
    {
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 키보드 제어
        float h = 0f;
        float v = 0f;

        if (Keyboard.current.aKey.isPressed) h = -1;
        if (Keyboard.current.dKey.isPressed) h = 1;
        if (Keyboard.current.wKey.isPressed) v = 1;
        if (Keyboard.current.sKey.isPressed) v = -1;

        moveTo = new Vector2(h, v);

        if (!GameManager.instance.isGameOver)
        {
            Shoot();    
        }
        
    }

    void Shoot()
    {
        if (Time.time - lastShotTime > shootInterval)
        {
            // 무기, 발사 위치, 회전없이
            Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity);

            lastShotTime = Time.time;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveTo * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" ||  other.tag == "Boss")
        {
            GameManager.instance.SaveWeaponSprite(GetCurrentWeaponSprite());
            GameManager.instance.SetGameOver();
            Destroy(gameObject);
        }
        else if (other.tag == "Coin")
        {
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject);
        }
    }

    public void UpgradeWeapon()
    {
        weaponIndex += 1;

        if (weapons.Length <= weaponIndex)
        {
            weaponIndex = weapons.Length - 1;
        }
    }
    
    public Sprite GetCurrentWeaponSprite()
    {
        SpriteRenderer sr = weapons[weaponIndex].GetComponent<SpriteRenderer>();
        if (sr != null)
            return sr.sprite;

        return null;
    }
 
}