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

        Debug.Log($"{gameObject.name}의 남은 HP : {hp}");
    }
    public virtual void Die() 
    {
        Debug.Log($"{gameObject.name}가 사망하였습니다.");
        Destroy(gameObject);
    }
}
