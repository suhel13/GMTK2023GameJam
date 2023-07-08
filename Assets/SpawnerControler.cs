using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerControler : MonoBehaviour
{
    public SpawnManager.spawnType type;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(this.transform.position);
        Debug.Log(type);
        Invoke("lateStartSpawn", 0.2f);
    }

    void lateStartSpawn()
    {
        GameManager.Instance.spawnManager.spawn(type, this.transform.position + Vector3.up);
    }
}