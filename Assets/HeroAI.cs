using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class HeroAI : MonoBehaviour, IdamageAble
{
    public float detectionRange = 3f;
    bool isAlive = true;
    
    public float hp;
    float maxHp;
    public float stamina;
    float maxStamina;

    public float attackCooldown;
    float cooldownTimer;
    public float staminaRecovery;
    public Image staminaBarImage;
    public Image hpbar;

    public float pickUpRange;
    public float speed;

    public float atackRange;
    public float atackStaminaCost;
    public float attackDamage;

    Rigidbody rb;
    GameObject closestCoin;
    float closestCoinDistance;

    GameObject closestEnemy;
    float closestEnemyDistance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        maxHp = hp;
        maxStamina = stamina;
    }
    private void Start()
    {
        GameManager.Instance.hero = this.transform;
    }
    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {

            if (stamina < maxStamina)
                stamina += staminaRecovery * Time.deltaTime;
            else if (stamina > maxStamina)
                stamina = maxStamina;
            staminaBarImage.fillAmount = stamina / maxStamina;
            hpbar.fillAmount = hp / maxHp;
            if (cooldownTimer < attackCooldown)
                cooldownTimer += Time.deltaTime;


            if (GameManager.Instance.spawnManager.coins.Count > 0)
            {
                Debug.Log("Searching for coin");
                closestCoinDistance = Mathf.Infinity;
                foreach (GameObject coin in GameManager.Instance.spawnManager.coins)
                {
                    if (Vector3.Distance(this.transform.position, coin.transform.position) < closestCoinDistance)
                    {
                        closestCoin = coin;
                        closestCoinDistance = Vector3.Distance(this.transform.position, closestCoin.transform.position);
                    }
                }
                moveForward(closestCoin.transform);
                if (closestCoinDistance <= pickUpRange)
                {
                    closestCoin.GetComponent<IPickAble>().pickUp();
                    closestCoin = null;
                }
            }
            else if (GameManager.Instance.spawnManager.enemes.Count > 0)
            {
                Debug.Log("Searching for enemys");
                closestEnemyDistance = Mathf.Infinity;
                foreach (GameObject enemy in GameManager.Instance.spawnManager.enemes)
                {
                    if (Vector3.Distance(this.transform.position, enemy.transform.position) < closestEnemyDistance)
                    {
                        closestEnemy = enemy;
                        closestEnemyDistance = Vector3.Distance(this.transform.position, closestEnemy.transform.position);
                    }
                }

                if (closestEnemyDistance > atackRange)
                {
                    moveForward(closestEnemy.transform);
                }
                else
                {
                    if (stamina >= atackStaminaCost && cooldownTimer >= attackCooldown)
                    {
                        closestEnemy.GetComponent<IdamageAble>().takeDamage(attackDamage);
                        stamina -= atackStaminaCost;
                        if (closestEnemy.GetComponent<EnemyAI>().getIsAlive() == false)
                            closestEnemy = null;
                        cooldownTimer = 0;
                    }
                }
            }
            else
            {
                moveForward(this.transform);
            }
        }
    }

    void moveForward(Transform target)
    {
        transform.LookAt(target);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
        rb.velocity = new Vector3(target.position.x - transform.position.x, 0, target.position.z - transform.position.z).normalized * speed;
    }

    public void takeDamage(float damage)
    {
        hp -= damage;
        if (hp < 0)
            death();
    }
    void death()
    {
        isAlive = false;
        Debug.Log("Hero dead Game Over");
    }
}
