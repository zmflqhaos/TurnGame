using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
 * ���⿡ �� �������ұ�
 * 6. ������ ����Ǿ��ִ� �����߿� �ϳ� ������� �Ǵ°Ű�, ������ ������ ������ �����ϴ� ���𰡰� �ʿ���
 * �ϴ� �������� �ϰ� ���Ŀ� �߰��ϱ�.
*/

public class BattleBase : MonoBehaviour
{
    [SerializeField]
    public AttackData[] testData; //���߿� �ٲ����

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
