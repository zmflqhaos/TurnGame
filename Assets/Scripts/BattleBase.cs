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

    public BattleBase[] target;

    public virtual void Start()
    {
        myBB = gameObject.GetComponent<BattleBase>();
        gameObject.transform.position = startPos;
        onTile = MapManager.Instance.SelectTile(startPos);
        onTile.OnTileChange(this);
        actionPoint = maxActionPoint;
    }

    public float SetFinalDamage(int num)
    {
        switch(myData.charAtd[num].damageType)
        {
            case AttackCategory.Physical:
                return myData.charAtd[num].damage * myData.charStat.strStat;
            case AttackCategory.Magic:
                return myData.charAtd[num].damage * myData.charStat.intStat;
            case AttackCategory.True:
                return myData.charAtd[num].damage;
        }
        Debug.LogError("������Ÿ���� �����ؾ��մϴ�!");
        return 0;
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
