using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : BattleBase
{
    [SerializeField]
    private NPCSO npcSo;

    public override void Start()
    {
        myData = npcSo.data;
        base.Start();
        hp = myData.charStat.vitStat * 2;
        mental = myData.charStat.mtlStat;
    }

    public override void Hit(float damage, AttackCategory damageType, AttackType attackType)
    {
        base.Hit(damage, damageType, attackType);
    }
}
