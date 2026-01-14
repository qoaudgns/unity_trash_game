using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI gameOverCoinText;
    private int coin = 0; // 코인 수

    [HideInInspector] public bool isGameOver = false;
    
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Image gameOverWeaponImage;
    private Sprite savedWeaponSprite;

    // start 함수보다 먼저 실행 됨
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void IncreaseCoin()
    {
        coin += 1;

        coinText.SetText(coin.ToString());

        // 코인 10개당 무기 업그레이드
        if (coin % 10 == 0)
        {
            Player player = FindAnyObjectByType<Player>();

            if (player != null)
            {
                player.UpgradeWeapon();
            }
        }
    }
    
    public void SaveWeaponSprite(Sprite sprite)
    {
        savedWeaponSprite = sprite;
    }

    public void SetGameOver()
    {
        isGameOver = true;
        
        EnemySpawner enemySpawner = FindAnyObjectByType<EnemySpawner>();

        if (enemySpawner != null)
        {
            enemySpawner.StopEnemyRoutine();
        }
        
        // 1초 후 게임오버 함수 실행
        Invoke("ShowGameOverPanel", 1f);
    }


    void ShowGameOverPanel()
    {
        gameOverWeaponImage.sprite = savedWeaponSprite;
        gameOverCoinText.SetText(coin.ToString());
        gameOverPanel.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("q1");
    }
}