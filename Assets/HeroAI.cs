using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HeroAI : MonoBehaviour
{
    
    public float detectionRange = 3f;

    public float pickUpRange;
    public float speed;

    Rigidbody rb;
    GameObject closestCoin;
    float closestCoinDistance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.spawnManager.coins.Count >= 0)
        {
            closestCoinDistance = Mathf.Infinity;
            foreach (GameObject coin in GameManager.Instance.spawnManager.coins)
            {
                if(Vector3.Distance(this.transform.position, coin.transform.position)< closestCoinDistance)
                {
                    closestCoin = coin;
                    closestCoinDistance = Vector3.Distance(this.transform.position, closestCoin.transform.position);
                }
            }
            moveForward(closestCoin.transform);
            Debug.Log(closestCoinDistance);
            if (closestCoinDistance <= pickUpRange)
            {
                closestCoin.GetComponent<IPickAble>().pickUp();
            }
        }
        else
        {
            moveForward(this.transform);
        }
    }

    void moveForward(Transform target)
    {
        transform.LookAt(target);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
        rb.velocity = new Vector3(target.position.x - transform.position.x, 0, target.position.z - transform.position.z).normalized * speed;
    }

}
