using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour, IPickAble
{
    public float coinScore;
    public void pickUp()
    {
        GameManager.Instance.score += coinScore;
        GameManager.Instance.spawnManager.coins.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

    public float rotaionSpeed;
    public float disapearTime;
    float disapearTimer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        disapearTimer += Time.deltaTime;
        if(disapearTimer > disapearTime)
        {
            Debug.Log("CoinDisapear",this);
        }
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + rotaionSpeed * Time.deltaTime, 90f);
    }
}
