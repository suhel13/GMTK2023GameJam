using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonAimPredictor : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileStartVelocity;
    public Transform crosshair;
    public LayerMask whatIsGround;
    GameObject tempProjectile;

    public Vector3 mousePosWorld;

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
            Physics.Raycast(Camera.main.ScreenPointToRay(ctx.ReadValue<Vector2>()), out mousePoint, Mathf.Infinity, whatIsGround);
            mousePosWorld = mousePoint.point;
            //Debug.Log(ctx.ReadValue<Vector2>());
            crosshair.position = mousePosWorld;
            Debug.DrawLine(this.transform.position, mousePosWorld);
        }
    }
}
