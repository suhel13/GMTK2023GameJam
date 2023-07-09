using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public enum spawnType { coin, enemyMele, enemyRange }
    public GameObject coinPrefab;
    public GameObject enemyMelePrefab;
    public GameObject enemyRangePrefab;

    public GameObject heroPrefab;

    public List<GameObject> coins = new List<GameObject>();
    public List<GameObject> enemes = new List<GameObject>();

    public List<spawnType> queue = new List<spawnType>();

    public QueueSpotControler queueSpot1;
    public QueueSpotControler queueSpot2;
    public QueueSpotControler queueSpot3;

    public GameObject spawnHeroStartPos()
    {
        return Instantiate(heroPrefab, Vector3.zero + Vector3.up * 1.5f, Quaternion.identity);
    }

    public GameObject spawn(spawnType type, Vector3 position)
    {
        GameObject temp = null;
        switch (type)
        {
            case spawnType.coin:
                temp = Instantiate(coinPrefab, position, Quaternion.identity);
                coins.Add(temp);
                break;
            case spawnType.enemyMele:
                temp = Instantiate(enemyMelePrefab, position, Quaternion.identity);
                enemes.Add(temp);
                break;
            case spawnType.enemyRange:
                temp = Instantiate(enemyRangePrefab, position, Quaternion.identity);
                enemes.Add(temp);
                break;
        }
        return temp;
    }

    public void updateQueueSpots()
    {
        queueSpot1.changeObject(queue[0]);
        queueSpot2.changeObject(queue[1]);
        queueSpot3.changeObject(queue[2]);
    }

    public void spawnNextInQueue(Vector3 position)
    {
        if(queue.Count == 0)
        {
            queue.Add(GameManager.Instance.generateNextEnemy());
        }
        spawn(queue[0], position);
        queue.RemoveAt(0);
        queue.Add(GameManager.Instance.generateNextEnemy());
    }
}