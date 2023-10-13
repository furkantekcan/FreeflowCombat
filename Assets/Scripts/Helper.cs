using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    private CombatScript playerCombat;

    private void Start()
    {
        playerCombat = GetComponentInParent<CombatScript>();
    }
    public void HitEvent()
    {
        playerCombat.HitEvent();
    }

    public void ResetAttack()
    {
        playerCombat.isAttackDone = true;
    }

    public void SetAttack()
    {
        playerCombat.isAttackDone = false;
    }
}