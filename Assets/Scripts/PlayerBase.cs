using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : BattleBase
{
    private GameObject[] moveAreas = new GameObject[4];
    [SerializeField]
    private GameObject moveArea;

    private GameObject[] attackAreas = new GameObject[10];
    [SerializeField]
    private GameObject attackArea;

    [SerializeField]
    private GameObject turnPanel;
    [SerializeField]
    private GameObject attackPanel;

    private LookRotation attackPos;
    private int attackNum;

    private Vector3 mousePos;
    private Vector3Int mousePosInt;

    private void Start()
    {
        myBB = gameObject.GetComponent<BattleBase>();
        actionPoint = maxActionPoint;

        gameObject.transform.position = BattleData.Instance.mapBox[myY][myX];
        BattleData.Instance.mapOnChar[myY][myX] = myBB;
        attackPos = LookRotation.D;
        for (int i = 0; i < 4; i++)
        {
            moveAreas[i] = Instantiate(moveArea, gameObject.transform);
            moveAreas[i].name = "MoveArea";
            moveAreas[i].transform.localPosition = BattleData.Instance.vecOne[i];
        }
        for (int i = 0; i < 10; i++)
        {
            attackAreas[i] = Instantiate(attackArea, gameObject.transform);
            attackAreas[i].name = "AttackArea";
        }
    }

    void Update()
    {
        AreaClickMove();
        AttackPosChange();
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (attackUIMode)
                ToggleAttackUI();
            if (attackRangeMode)
            {
                ClearAttackArea();
                ToggleAttackArea();
            }
        }
        if (attackArea.activeSelf)
        {
            Attack();
        }
    }

    #region ����
    private void ToggleMoveArea()
    {
        moveArea.SetActive(moveMode);
        if (moveMode)
        {
            for (int i = 0; i < 4; i++)
                moveAreas[i].SetActive(CheckOutLine(i));
        }
        else
        {
            for (int i = 0; i < 4; i++)
                moveAreas[i].SetActive(false);
        }
    }

    private void AreaClickMove()
    {
        if (!Input.GetMouseButtonDown(0) || !moveMode) return;
        mousePos = BattleData.Instance.mainCam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
        mousePosInt = Vector3Int.RoundToInt(mousePos);
        for (int i = 0; i < 4; i++)
        {
            if (mousePosInt == BattleData.Instance.mapBox[myY][myX] + BattleData.Instance.vecOne[i] && CheckOutLine(i) && !BattleData.Instance.mapOnChar[myY + BattleData.Instance.vecOne[i].y][myX + BattleData.Instance.vecOne[i].x])
            {
                BattleData.Instance.mapOnChar[myY][myX] = null;
                myX += BattleData.Instance.vecOne[i].x;
                myY += BattleData.Instance.vecOne[i].y;
                BattleData.Instance.mapOnChar[myY][myX] = myBB;
                gameObject.transform.position = BattleData.Instance.mapBox[myY][myX];
                actionPoint--;
                moveMode = false;
                break;
            }
        }
        ToggleMoveArea();
    }

    private bool CheckOutLine(int i)
    {
        return myX + BattleData.Instance.vecOne[i].x >= 0 && myX + BattleData.Instance.vecOne[i].x < BattleData.Instance.mapSize.x && myY + BattleData.Instance.vecOne[i].y >= 0 && myY + BattleData.Instance.vecOne[i].y < BattleData.Instance.mapSize.y;
    }
    #endregion

    #region ���� UI
    private void AttackPosChange()
    {
        if (actionPoint < testData[attackNum].useAP) return;
        if (!attackUIMode && attackRangeMode)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                attackPos = LookRotation.W;
                ChangeAttackArea(attackNum);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                attackPos = LookRotation.A;
                ChangeAttackArea(attackNum);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                attackPos = LookRotation.S;
                ChangeAttackArea(attackNum);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                attackPos = LookRotation.D;
                ChangeAttackArea(attackNum);
            }
        }
    }

    public void ChangeAttackArea(int num)
    {
        attackNum = num;
        if (actionPoint < testData[num].useAP) return;
        AttackAreaRotation();
    }

    private void AttackAreaRotation()
    {
        Vector3Int vec = new Vector3Int();
        switch (attackPos)
        {
            case LookRotation.W:
                for (int i = 0; i < testData[attackNum].attackRange.Count; i++)
                {
                    vec.Set(-testData[attackNum].attackRange[i].y, testData[attackNum].attackRange[i].x, 0);
                    attackAreas[i].transform.position = BattleData.Instance.mapBox[myY][myX] + vec;
                    attackAreas[i].SetActive(true);
                }
                break;
            case LookRotation.A:
                for (int i = 0; i < testData[attackNum].attackRange.Count; i++)
                {
                    vec.Set(-testData[attackNum].attackRange[i].x, -testData[attackNum].attackRange[i].y, 0);
                    attackAreas[i].transform.position = BattleData.Instance.mapBox[myY][myX] + vec;
                    attackAreas[i].SetActive(true);
                }
                break;
            case LookRotation.S:
                for (int i = 0; i < testData[attackNum].attackRange.Count; i++)
                {
                    vec.Set(testData[attackNum].attackRange[i].y, -testData[attackNum].attackRange[i].x, 0);
                    attackAreas[i].transform.position = BattleData.Instance.mapBox[myY][myX] + vec;
                    attackAreas[i].SetActive(true);
                }
                break;
            case LookRotation.D:
                for (int i = 0; i < testData[attackNum].attackRange.Count; i++)
                {
                    vec.Set(testData[attackNum].attackRange[i].x, testData[attackNum].attackRange[i].y, 0);
                    attackAreas[i].transform.position = BattleData.Instance.mapBox[myY][myX] + vec;
                    attackAreas[i].SetActive(true);
                }
                break;
        }
        attackArea.SetActive(true);
    }

    private void ClearAttackArea()
    {
        attackArea.SetActive(false);
        for (int i = 0; i < 10; i++)
        {
            attackAreas[i].SetActive(false);
        }
    }

    private void Attack()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        mousePos = BattleData.Instance.mainCam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
        mousePosInt = Vector3Int.RoundToInt(mousePos);

        for (int i = 0; i < attackAreas.Length; i++)
        {
            if (!attackAreas[i].activeSelf)
            {
                Debug.Log($"{i}������ Ż��!");
                break;
            }
            if (CheckAttackOutLine(i))
            {
                if (BattleData.Instance.mapOnChar[mousePosInt.y][mousePosInt.x] != null)
                {
                    if (attackAreas[i].transform.position == BattleData.Instance.mapOnChar[mousePosInt.y][mousePosInt.x]?.transform.position)
                    {
                        BattleData.Instance.mapOnChar[mousePosInt.y][mousePosInt.x].Hit(testData[attackNum].damage);
                        actionPoint -= testData[attackNum].useAP;
                        attackRangeMode = false;
                        GoToMoveUI();
                        break;
                    }
                }
            }
        }
    }

    private bool CheckAttackOutLine(int i)
    {
        float x = attackAreas[i].transform.localPosition.x;
        float y = attackAreas[i].transform.localPosition.y;
        return x >= 0 && x < BattleData.Instance.mapSize.x && y >= 0 && y < BattleData.Instance.mapSize.y;
    }

    #endregion

    public override void Hit(float damage)
    {
        Debug.Log($"{damage}�� ������!");
    }
    public void MyTurn()
    {
        actionPoint = maxActionPoint;
        maxActionPoint = 3;
    }

    public void Move()
    {
        if (actionPoint <= 0) return;
        moveMode = !moveMode;
        ToggleMoveArea();
    }

    public void ToggleAttackUI()
    {
        if (actionPoint <= 0) return;
        attackUIMode = !attackUIMode;
        moveMode = false;
        attackRangeMode = false;
        attackPanel.SetActive(attackUIMode);
        turnPanel.SetActive(!attackUIMode);
        ToggleMoveArea();
        ClearAttackArea();
    }

    public void ToggleAttackArea()
    {
        if (actionPoint < testData[attackNum].useAP) return;
        attackUIMode = !attackUIMode;
        attackRangeMode = !attackRangeMode;
        moveMode = false;
        attackPanel.SetActive(attackUIMode);
    }

    private void GoToMoveUI()
    {
        ClearAttackArea();
        attackUIMode = false;
        attackRangeMode = false;
        moveMode = false;
        attackPanel.SetActive(false);
        turnPanel.SetActive(true);
    }
}