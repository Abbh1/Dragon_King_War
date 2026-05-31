using UnityEngine;

public class SpawnEnemyPoint : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxAmount;//最大怪物数目
    public float spawnDistance;// 刷新距离
    public float spawnSpan;//刷新间隔
    private int presentAmount;
    private float timer;
    private float lastSpawnTime;
    private void Update()
    {
        timer += Time.deltaTime;
        if(presentAmount<maxAmount&&timer-lastSpawnTime>spawnSpan)
        {
            SpawnEnemy();
            lastSpawnTime = timer;
        }
    }
    private void SpawnEnemy()
    {
        GameObject obj=Instantiate(enemyPrefab, Utils.GetRandomPositionAroundCenter(spawnDistance, transform.position), Quaternion.identity);
        obj.GetComponent<Enemy>().bornPlace = transform.position;
        presentAmount++;
    }
}
