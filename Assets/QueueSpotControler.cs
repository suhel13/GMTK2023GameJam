using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueSpotControler : MonoBehaviour
{
    GameObject activeObject;

    public void changeObject(SpawnManager.spawnType type)
    {
        Destroy(activeObject);
        switch (type)
        {
            case SpawnManager.spawnType.coin :
                activeObject = GameManager.Instance.spawnManager.spawn(SpawnManager.spawnType.coin, this.transform.position + Vector3.up * 0.5f);
                GameManager.Instance.spawnManager.coins.Remove(activeObject);
                break;

            case SpawnManager.spawnType.enemyMele:
                activeObject = GameManager.Instance.spawnManager.spawn(SpawnManager.spawnType.enemyMele, this.transform.position + Vector3.up * 0.5f);
                activeObject.GetComponent<EnemyAI>().isAlive = false;
                GameManager.Instance.spawnManager.enemes.Remove(activeObject);
                break;

            case SpawnManager.spawnType.enemyRange:
                activeObject = GameManager.Instance.spawnManager.spawn(SpawnManager.spawnType.enemyRange, this.transform.position + Vector3.up * 0.5f);
                activeObject.GetComponent<EnemyAI>().isAlive = false;
                GameManager.Instance.spawnManager.enemes.Remove(activeObject);
                break;
        }
    }
}
