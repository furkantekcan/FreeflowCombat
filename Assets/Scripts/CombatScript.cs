using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    private InputManager inputManager;
    private Animator animator;
    private CinemachineImpulseSource impulseSource;
    private EnemyDetection enemyDetection;
    private PlayerLocomotion playerLocomotion;

    [Header("Target")]
    private GameObject lockedTarget;

    [Header("Combat Settings")]
    [SerializeField] private float attackCooldown;

    [Header("States")]
    public bool isAttackingEnemy = false;
    public bool isCountering = false;

    [Header("Public References")]
    [SerializeField] private Transform punchPosition;
    //[SerializeField] private ParticleSystemScript punchParticle;
    [SerializeField] private GameObject lastHitCamera;
    [SerializeField] private Transform lastHitFocusObject;


    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        enemyDetection = GetComponent<EnemyDetection>();
        inputManager = GetComponent<InputManager>();
        impulseSource = GetComponentInChildren<CinemachineImpulseSource>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    public void AttackCheck()
    {
        if (enemyDetection.currentTarget == null)
        {
            return;
        }

        else
        {
            MoveTowardsTarget(enemyDetection.currentTarget.transform,1);
        }
    }

    private void MoveTowardsTarget(Transform target, float duration)
    {
        transform.DOLookAt(target.position, 0.2f);
        transform.DOMove(TargetOffset(target), duration);
    }

    public Vector3 TargetOffset(Transform target)
    {
        Vector3 position;
        position = target.position;
        return Vector3.MoveTowards(position, transform.position, .95f);
    }
}
