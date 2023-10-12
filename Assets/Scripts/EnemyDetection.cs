using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private InputManager inputManager;

    public LayerMask layerMask;

    [HideInInspector] public Vector3 inputDirection;

    [SerializeField] public GameObject currentTarget;

    private void Start()
    {
        inputManager = GetComponent<InputManager>();
    }

    private void Update()
    {
        var camera = Camera.main;
        var forward = camera.transform.forward;
        var right = camera.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        inputDirection = forward * inputManager.verticalInput + right * inputManager.horizontalInput;
        inputDirection.Normalize();


        RaycastHit info;

        if (Physics.SphereCast(transform.position, 3f, inputDirection, out info, 10, layerMask))
        {
            if (info.collider.gameObject.layer == 6)
            {
                Debug.Log("ENEMY");
                currentTarget = info.collider.gameObject;
               // OnDrawGizmos();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, inputDirection);
        Gizmos.DrawWireSphere(transform.position, 1);
        if( currentTarget != null)
            Gizmos.DrawSphere(currentTarget.transform.position, .5f);
    }
}
