using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonControler : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileStartVelocity;
    public Transform crosshair;
    public LayerMask whatIsGround;
    GameObject tempProjectile;

    public Vector3 mousePosWorld;

    [Range(2,5)] public float spawnHeight;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void getMausePos(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            RaycastHit mousePoint;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(ctx.ReadValue<Vector2>()), out mousePoint, Mathf.Infinity, whatIsGround))
            {
                mousePosWorld = mousePoint.point;
                crosshair.position = mousePosWorld;
            }
            //Debug.Log(ctx.ReadValue<Vector2>());
            Debug.DrawLine(this.transform.position, mousePosWorld);
        }
    }

    public void shoot(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            GameManager.Instance.spawnManager.spawn(SpawnManager.spawnType.coin, crosshair.position + (Vector3.up * spawnHeight));
        }
    }
}
