using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public enum spawnType { coin, enemyMele, enemyRange}
    public GameObject coinPrefab;
    public GameObject enemyMelePrefab;
    public GameObject enemyRangePrefab;


    public GameObject spawn(spawnType type, Vector3 position)
    {
        GameObject temp =  null;
        switch (type)
        {
            case spawnType.coin:
                temp = Instantiate(coinPrefab, position, Quaternion.identity);
                break;
            case spawnType.enemyMele:
                temp = Instantiate(enemyMelePrefab, position, Quaternion.identity);
                break;
            case spawnType.enemyRange:
                temp = Instantiate(enemyRangePrefab, position, Quaternion.identity);
                break;
        }
        return temp;
    }
}
