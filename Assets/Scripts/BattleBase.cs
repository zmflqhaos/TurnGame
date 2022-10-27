using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 여기에 뭘 만들어야할까
 * 6. 스텟은 저장되어있는 스텟중에 하나 때어오면 되는거고, 누구의 스텟을 들고올지 구분하는 무언가가 필요해
 * 일단 이정도만 하고 이후에 추가하기.
*/

public class BattleBase : MonoBehaviour
{
    [HideInInspector]
    public BattleBase myBB;
    [HideInInspector]
    public CharactorData myData;

    public Vector3Int startPos;

    public bool isPlayerTeam;

    public int actionPoint;
    public int maxActionPoint;

    public Tile onTile;

    public int hp;
    public int mental;

    public List<AttackSO> attacks;
    public BattleBase[] target;

    public virtual void Start()
    {
        myBB = gameObject.GetComponent<BattleBase>();
        gameObject.transform.position = startPos;
        for (int i = 0; i < myData.charAtd.Count; i++)
        {
            attacks.Add(GameManager.Instance.attackSOList[myData.charAtd[i]]);
        }
        onTile = MapManager.Instance.SelectTile(startPos);
        onTile.OnTileChange(this);
        actionPoint = maxActionPoint;
    }

    public float SetFinalDamage(int num)
    {
        float finalDamage = 0;
        switch(attacks[num].damageType)
        {
            case AttackCategory.Physical:
                finalDamage = attacks[num].damage * myData.charStat.strStat;
                break;
            case AttackCategory.Magic:
                finalDamage = attacks[num].damage * myData.charStat.intStat;
                break;
            case AttackCategory.True:
                finalDamage = attacks[num].damage;
                break;
        }
        finalDamage = Mathf.Round(finalDamage);
        if (finalDamage < 1) finalDamage = 1;
        return finalDamage;
    }

    public virtual void Hit(float damage, AttackCategory damageType, AttackType attackType) 
    {
        Debug.Log($"{damageType}으로 {damage}의 공격!");
        hp -= (int)damage;

        if (hp <= 0)
        {
            Die();
            return;
        }

        Debug.Log($"{gameObject.name}의 남은 HP : {hp}");
    }

    public virtual void Die() 
    {
        Debug.Log($"{gameObject.name}가 사망하였습니다.");
        onTile.OnTileChange();
        Destroy(gameObject);
    }
}
