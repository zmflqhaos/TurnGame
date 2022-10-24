using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : BattleBase
{
    [SerializeField]
    private NPCSO myData;
    public override void Start()
    {
        base.Start();
        hp = 100;
    }

    public override void Hit(float damage, AttackCategory damageType)
    {
        base.Hit(damage, damageType);
    }
}
