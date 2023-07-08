using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonAimPredictor : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileStartVelocity;

    GameObject tempProjectile;

    public float timeScale;
    public Vector3 mausePosWorld;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(tempProjectile == null)
        {
            tempProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }
    }

    public void getMausePos(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            RaycastHit mousePoint;
            Physics.Raycast(Camera.main.ScreenPointToRay(ctx.ReadValue<Vector2>()), out mousePoint);
            mausePosWorld = mousePoint.point; 
            //Debug.Log(ctx.ReadValue<Vector2>());
            Debug.Log(Camera.main.transform.position);
            Debug.Log(ctx.ReadValue<Vector2>());
            Debug.DrawLine(this.transform.position, mausePosWorld);
        }
    }
}
