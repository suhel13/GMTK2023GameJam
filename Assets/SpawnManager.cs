using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public enum spawnType { coin, enemyMele, enemyRange}
    public GameObject coinPrefab;
    public GameObject enemyMelePrefab;
    public GameObject enemyRangePrefab;

    public List<GameObject> coins = new List<GameObject> ();
    public List<GameObject> enemesMele = new List<GameObject> ();
    public List<GameObject> enemesRange = new List<GameObject> ();

    public GameObject spawn(spawnType type, Vector3 position)
    {
        GameObject temp =  null;
        switch (type)
        {
            case spawnType.coin:
                temp = Instantiate(coinPrefab, position, Quaternion.identity);
                Debug.Log(temp);
                coins.Add(temp);
                Debug.Log(coins.Count);
                break;
            case spawnType.enemyMele:
                temp = Instantiate(enemyMelePrefab, position, Quaternion.identity);
                enemesMele.Add(temp);
                break;
            case spawnType.enemyRange:
                temp = Instantiate(enemyRangePrefab, position, Quaternion.identity);
                enemesRange.Add(temp);
                break;
        }
        return temp;
    }
}
