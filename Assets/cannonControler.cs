using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CannonControler : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileStartVelocity;
    public Transform crosshair;
    public LayerMask whatIsGround;
    GameObject tempProjectile;

    public float minPitch;
    public float maxPitch;

    public Vector3 mousePosWorld;

    [Range(2,5)] public float spawnHeight;

    public float maxTimeNoShoot;
    float NoShootTimer;
    public Image maxTimeImage;

    public Transform cannonBase;
    public Transform cannonBarel;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(cannonBase);
        Debug.Log(cannonBarel);
        Debug.Log("",this);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.uiMan.state == UI_manager.gameState.play)
        {
            NoShootTimer += Time.deltaTime;
            if(NoShootTimer > maxTimeNoShoot)
            {
                forcedShoot();
                NoShootTimer = 0;
            }
            maxTimeImage.fillAmount = 1 - (NoShootTimer / maxTimeNoShoot);
        }
    }

    public void getMausePos(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && Time.timeScale > 0 && GameManager.Instance.uiMan.state == UI_manager.gameState.play)
        {
            RaycastHit mousePoint;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(ctx.ReadValue<Vector2>()), out mousePoint, Mathf.Infinity, whatIsGround))
            {
                mousePosWorld = mousePoint.point;
                crosshair.position = mousePosWorld;
            }
            //Debug.Log(ctx.ReadValue<Vector2>());
            Debug.DrawLine(this.transform.position, mousePosWorld);
            lookAt(crosshair);
        }
    }
    void forcedShoot()
    {
        GameManager.Instance.spawnManager.spawnNextInQueue(crosshair.position + (Vector3.up * spawnHeight));
    }
    public void shoot(InputAction.CallbackContext ctx)
    {
        if(Time.timeScale> 0 && ctx.performed && GameManager.Instance.uiMan.state == UI_manager.gameState.play)
        {
            GameManager.Instance.spawnManager.spawnNextInQueue(crosshair.position + (Vector3.up * spawnHeight));
            NoShootTimer = 0;
        }
    }

    void lookAt(Transform target)
    {
        if(Time.timeScale > 0 && GameManager.Instance.uiMan.state == UI_manager.gameState.play)
        {
            cannonBase.eulerAngles = new Vector3( cannonBase.eulerAngles.x, Mathf.Atan2(target.position.x - this.transform.position.x, target.position.z - this.transform.position.z) * 180f / Mathf.PI, cannonBase.eulerAngles.z);
            cannonBase.eulerAngles = new Vector3( cannonBase.eulerAngles.x, Mathf.Atan2(target.position.x - this.transform.position.x, target.position.z - this.transform.position.z) * 180f / Mathf.PI, cannonBase.eulerAngles.z);
            cannonBarel.eulerAngles = new Vector3( minPitch - new Vector2(target.position.z - this.transform.position.z, target.position.x - this.transform.position.x).magnitude / Mathf.Sqrt(400f + 100f) * (maxPitch- minPitch), cannonBarel.eulerAngles.y, cannonBarel.eulerAngles.z);
        }
    }
}
