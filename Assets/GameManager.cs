using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public SpawnManager spawnManager;
    public Transform hero;

    public List<int> levelThreshold = new List<int>();

    public float score;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GetComponentInChildren<SpawnManager>();
        spawnManager.queue.Add(generateNextEnemy());
        spawnManager.queue.Add(generateNextEnemy());
        spawnManager.queue.Add(generateNextEnemy());
        spawnManager.updateQueueSpots();
    }

    public void restartGame()
    {
        score = 0;

        spawnManager.queue.Clear();
        spawnManager.queue.Add(generateNextEnemy());
        spawnManager.queue.Add(generateNextEnemy());
        spawnManager.queue.Add(generateNextEnemy());
        spawnManager.updateQueueSpots();

        foreach(GameObject coins in spawnManager.coins)
        {
            Destroy(coins);
        }
        foreach(GameObject enemy in spawnManager.enemes)
        { 
            Destroy(enemy);
        }

        Destroy(hero.gameObject);
        hero = spawnManager.spawnHeroStartPos().transform;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public SpawnManager.spawnType generateNextEnemy()
    {
        int rand = Random.Range(0, 100);

        if (score > levelThreshold[2])
        {
            //Level 4
            if (rand > 60)
                return SpawnManager.spawnType.enemyRange;
            else if (rand > 30)
                return SpawnManager.spawnType.enemyMele;
            else
                return SpawnManager.spawnType.coin;

        }
        else if (score > levelThreshold[1])
        {
            //Level 3
            if (rand > 70)
                return SpawnManager.spawnType.enemyRange;
            else if (rand > 40)
                return SpawnManager.spawnType.enemyMele;
            else
                return SpawnManager.spawnType.coin;

        }
        else if (score > levelThreshold[0])
        {
            //Level 2
            if (rand > 80)
                return SpawnManager.spawnType.enemyRange;
            else if (rand > 50)
                return SpawnManager.spawnType.enemyMele;
            else
                return SpawnManager.spawnType.coin;
        }
        else
        {
            //Level 1
            if (rand > 60)
                return SpawnManager.spawnType.enemyMele;
            else
                return SpawnManager.spawnType.coin;
        }
    }
}
