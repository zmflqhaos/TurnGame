using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���⿡ �� �������ұ�
 * 6. ������ ����Ǿ��ִ� �����߿� �ϳ� ������� �Ǵ°Ű�, ������ ������ ������ �����ϴ� ���𰡰� �ʿ���
 * �ϴ� �������� �ϰ� ���Ŀ� �߰��ϱ�.
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
        Debug.Log($"{damageType}���� {damage}�� ����!");
        hp -= (int)damage;

        if (hp <= 0)
        {
            Die();
            return;
        }

        Debug.Log($"{gameObject.name}�� ���� HP : {hp}");
    }

    public virtual void Die() 
    {
        Debug.Log($"{gameObject.name}�� ����Ͽ����ϴ�.");
        onTile.OnTileChange();
        Destroy(gameObject);
    }
}
