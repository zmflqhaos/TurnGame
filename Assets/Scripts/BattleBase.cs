using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
 * 여기에 뭘 만들어야할까
 * 6. 스텟은 저장되어있는 스텟중에 하나 때어오면 되는거고, 누구의 스텟을 들고올지 구분하는 무언가가 필요해
 * 일단 이정도만 하고 이후에 추가하기.
*/

public class BattleBase : MonoBehaviour
{
    [SerializeField]
    public AttackData[] testData; //나중에 바꿔야해

    public BattleBase myBB;
    public int myX;
    public int myY;

    public bool isPlayerTeam = true;
    public bool moveMode = false;
    public bool attackUIMode = false;
    public bool attackRangeMode = false;

    public int actionPoint;
    public int maxActionPoint = 3;

    public virtual void Hit(float damage) { }
}
