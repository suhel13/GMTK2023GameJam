using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IdamageAble
{
    public float hp;
    bool isAlive = true;
    public float speed;
    public float attackRange;
    public float attackDamage;
    public float attackCooldown;
    
    float cooldownTimer;

    public float arrowSpeed;
    public Transform shootingPoint;
    public GameObject arrowPrefab;
    public enum enemyType {melle, range}
    public enemyType type;

    Rigidbody rb;

    public void takeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
            death();
    }

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            if (cooldownTimer < attackCooldown)
                cooldownTimer += Time.deltaTime;

            Debug.Log(GameManager.Instance.hero);
            moveForward(GameManager.Instance.hero);
        }
    }
    void death()
    {
        Debug.Log("This Oponent is dead.", this.gameObject);
        isAlive = false;
        GameManager.Instance.spawnManager.enemes.Remove(this.gameObject);
        Destroy(this.gameObject);
    }
    public bool getIsAlive()
    {
        return isAlive;
    }
    void moveForward(Transform target)
    {
        transform.LookAt(target);
        if (Vector3.Distance(this.transform.position, target.position) > attackRange)
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
            rb.velocity = new Vector3(target.position.x - transform.position.x, 0, target.position.z - transform.position.z).normalized * speed;
        }
        else
        {
            switch(type)
            {
                case enemyType.melle:
                    if (cooldownTimer >= attackCooldown)
                    {
                        rb.velocity = Vector3.zero;
                        GameManager.Instance.hero.GetComponent<IdamageAble>().takeDamage(attackDamage);
                        cooldownTimer = 0;
                    }
                    break;
                case enemyType.range:
                    if (cooldownTimer >= attackCooldown)
                    {
                        rb.velocity = Vector3.zero;
                        GameObject tempArrow = Instantiate(arrowPrefab, shootingPoint.position, transform.rotation);
                        tempArrow.GetComponent<Rigidbody>().velocity = new Vector3(target.position.x - transform.position.x, 0, target.position.z - transform.position.z).normalized * arrowSpeed;
                        tempArrow.GetComponent<ArrowControler>().damage = attackDamage;
                        cooldownTimer = 0;
                    }
                    break;
            }
        }
    }
}
