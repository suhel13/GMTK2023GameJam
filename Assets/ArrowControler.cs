using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControler : MonoBehaviour
{
    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public float damage;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Arrow hit", collision.gameObject);

        if(collision.gameObject.GetComponent<IdamageAble>() != null)
        {
            collision.gameObject.GetComponent<IdamageAble>().takeDamage(damage);
            if (collision.gameObject.tag == "Enemy")
                GameManager.Instance.score += 10;
        }
        
        this.GetComponent<BoxCollider>().enabled = false;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        this.transform.SetParent(collision.transform);
    }
}
