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
    public BattleBase myBB;
    public Vector3Int myPos;

    public bool isPlayerTeam;
    public bool moveMode;
    public bool attackUIMode;
    public bool attackRangeMode;

    public int actionPoint;
    public int maxActionPoint;

    public int hp;
    public float finalDamage;

    public virtual void Start()
    {
        myBB = gameObject.GetComponent<BattleBase>();

        gameObject.transform.position = myPos;
    }

    public virtual void Hit(float damage) 
    {
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
        Destroy(gameObject);
    }
}
