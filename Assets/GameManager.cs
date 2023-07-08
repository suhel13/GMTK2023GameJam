using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public SpawnManager spawnManager;

    [Range(0, 0.1f)] public float mouseSensitivity = 0.05f;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
