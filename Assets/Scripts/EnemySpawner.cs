using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies; // 적군들
    [SerializeField] private GameObject boss; // 보스
    [SerializeField] private float spawnInterval = 1.5f; // 적 생성 주기

    private float[] arrPosX = { -2.2f, -1.1f, 0f, 1.1f, 2.2f }; // 적 생성 위치


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartEnemyRoutine();
    }

    void StartEnemyRoutine()
    {
        StartCoroutine("EnemyRoutine");
    }

    public void StopEnemyRoutine()
    {
        StopCoroutine("EnemyRoutine");
    }

    IEnumerator EnemyRoutine()
    {
        yield return new WaitForSeconds(3f);

        float moveSpeed = 5f; // 적 이동 속도
        int spawnCount = 0; // 적 웨이브
        int enemyIndex = 0; // 적 레벨

        while (true)
        {
            foreach (float posX in arrPosX)
            {
                // 적 랜덤 생성
                // int index = Random.Range(0, enemyIndex);
                SpawnEnemy(posX, enemyIndex, moveSpeed);
            }

            spawnCount++;
            if (spawnCount % 10 == 0)
            {
                enemyIndex++;
                moveSpeed += 2;
            }

            if (enemyIndex >= enemies.Length)
            {
                SpawnBoss();

                enemyIndex = 0;
                moveSpeed = 5f;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnEnemy(float posX, int index, float moveSpeed)
    {
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);

        if (Random.Range(0, 5) == 0)
        {
            index += 1;
        }

        if (Random.Range(0, 7) == 0)
        {
            index -= 1;
        }

        if (index >= enemies.Length)
        {
            index = enemies.Length - 1;
        }
        else if (index < 0)
        {
            index = 1;
        }

        GameObject enemyObject = Instantiate(enemies[index], spawnPos, Quaternion.identity);
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.SetMoveSpeed(moveSpeed);
    }

    void SpawnBoss()
    {
        Instantiate(boss, transform.position, Quaternion.identity);
    }
}