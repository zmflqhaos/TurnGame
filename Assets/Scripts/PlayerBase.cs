using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : BattleBase
{
    private CharactorData myData;

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

    public bool moveMode;
    public bool attackUIMode;
    public bool attackRangeMode;

    private Vector3 mousePos;
    private Vector3Int mousePosInt;

    private int count;

    public override void Start()
    {
        base.Start();
        myData = GameManager.Instance.CurrentGameData._playersData[0];
        attackPos = LookRotation.D;
        count = 0;
        for (int i = 0; i < 4; i++)
        {
            moveAreas[i] = Instantiate(moveArea, gameObject.transform);
            moveAreas[i].name = "MoveArea";
            moveAreas[i].transform.localPosition = MapManager.Instance.vecOne[i];
        }
        for (int i = 0; i < 10; i++)
        {
            attackAreas[i] = Instantiate(attackArea, gameObject.transform);
            attackAreas[i].name = "AttackArea";
        }
    }

    void Update()
    {
        InputMouse();
        AttackPosChange();
        Tab();
        Attack();
    }

    private void InputMouse()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        mousePos = GameManager.Instance.mainCam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
        mousePosInt = Vector3Int.RoundToInt(mousePos);
        if (attackArea.activeSelf) SelectTarget();
        else Move();
    }

    #region 무브
    private void ToggleMoveArea()
    {
        moveArea.SetActive(moveMode);
        turnPanel.SetActive(!moveMode);
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

    private void Move()
    {
        for (int i = 0; i < 4; i++)
        {
            if (mousePosInt == onTile.transform.position + MapManager.Instance.vecOne[i] && CheckOutLine(i))
            {
                onTile.OnTileChange();
                onTile = MapManager.Instance.SelectTile(onTile.transform.position + MapManager.Instance.vecOne[i]);
                onTile.OnTileChange(this);

                gameObject.transform.position = onTile.transform.position;

                actionPoint--;
                moveMode = false;
                break;
            }
        }
        ToggleMoveArea();
    } 

    private bool CheckOutLine(int i)
    {
        return onTile.transform.position.x + MapManager.Instance.vecOne[i].x >= 0
            && onTile.transform.position.x + MapManager.Instance.vecOne[i].x <= MapManager.Instance.mapTiles.GetLength(1)
            && onTile.transform.position.y + MapManager.Instance.vecOne[i].y >= 0
            && onTile.transform.position.y + MapManager.Instance.vecOne[i].y <= MapManager.Instance.mapTiles.GetLength(0);
    }
    #endregion

    #region 어택 UI
    private void AttackPosChange()
    {
        if (actionPoint < myData._charAtd[attackNum].useAP) return;
        if (!attackUIMode && attackRangeMode)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                attackPos = LookRotation.W;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                attackPos = LookRotation.A;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                attackPos = LookRotation.S;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                attackPos = LookRotation.D;
            }
            AttackAreaRotation();
        }
    }

    //버튼용
    public void ChangeAttackArea(int num)
    {
        attackNum = num;
        if (actionPoint < myData._charAtd[num].useAP) return;
        target = new BattleBase[myData._charAtd[num].splashCount];
        AttackAreaRotation();
    }

    private void AttackAreaRotation()
    {
        switch (attackPos)
        {
            case LookRotation.W:
                AreaSetting(1, -1, false);
                break;
            case LookRotation.A:
                AreaSetting(-1, -1, true);
                break;
            case LookRotation.S:
                AreaSetting(-1, 1, false);
                break;
            case LookRotation.D:
                AreaSetting(1, 1, true);
                break;
        }
        attackArea.SetActive(true);
    }

    private void AreaSetting(int x, int y, bool isXfirst)
    {
        Vector3Int vec = new Vector3Int();
        for (int i = 0; i < myData._charAtd[attackNum].attackRange.Count; i++)
        {
            if(isXfirst)
            vec.Set((myData._charAtd[attackNum].attackRange[i].x * x), (myData._charAtd[attackNum].attackRange[i].y * y), 0);
            else
            vec.Set((myData._charAtd[attackNum].attackRange[i].y * y), (myData._charAtd[attackNum].attackRange[i].x * x), 0);
            attackAreas[i].transform.position = gameObject.transform.position + vec;
            if()
            attackAreas[i].SetActive(true);
        }
    }

    /*private bool CheckOutLine(int i)
    {
        return onTile.transform.position.x + MapManager.Instance.vecOne[i].x >= 0
            && onTile.transform.position.x + MapManager.Instance.vecOne[i].x <= MapManager.Instance.mapTiles.GetLength(1)
            && onTile.transform.position.y + MapManager.Instance.vecOne[i].y >= 0
            && onTile.transform.position.y + MapManager.Instance.vecOne[i].y <= MapManager.Instance.mapTiles.GetLength(0);
    }*/

    private void ClearAttackArea()
    {
        attackArea.SetActive(false);
        for (int i = 0; i < 10; i++)
        {
            attackAreas[i].SetActive(false);
        }
    }

    private void SelectTarget()
    {
        for (int i = 0; i < attackAreas.Length; i++)
        {
            if (!attackAreas[i].activeSelf) break;

            if (attackAreas[i].transform.position == mousePosInt)
            {
                CheckInTarget(MapManager.Instance.SelectTile(mousePosInt)?.onTile);
            }
        }
    }

    private bool CheckInTarget(BattleBase battle)
    {
        if (battle == null) return false;
        for(int i=0; i<target.Length; i++)
        {
            if (target[i] == null) break;
            if (target[i].gameObject == battle.gameObject)
            {
                for (int j = i + 1; j < target.Length; j++)
                {
                    target[j - 1] = target[j];
                }
                target[count] = null;
                count--;
                Debug.Log($"타겟 해제 : {battle.gameObject.name}, 남은 선택 수 {target.Length - count}");
                return false;
            }
        }
        if (count >= target.Length) return false;
        target[count] = battle;
        count++;
        Debug.Log($"타게팅 : {battle.gameObject.name}, 남은 선택 수 {target.Length-count}");
        return true;
    }

    private void Attack()
    {
        if (!Input.GetKeyDown(KeyCode.Return) || count <= 0) return;

        foreach (BattleBase a in target)
        {
            a?.Hit(myData._charAtd[attackNum].damage, myData._charAtd[attackNum].damageType);
        }
        GoToTurnUI();
    }

    #endregion

    public override void Hit(float damage, AttackCategory damageType)
    {
        base.Hit(damage, damageType);
    }

    //버튼용
    public void MyTurn()
    {
        actionPoint = maxActionPoint;
        maxActionPoint = 3;
    }

    //버튼용
    public void ActiveMove()
    {
        if (actionPoint <= 0) return;
        moveMode = true;
        ToggleMoveArea();
    }

    public void Tab()
    {
        if (!Input.GetKeyDown(KeyCode.Tab)) return;

        if (attackUIMode)
        {
            turnPanel.SetActive(true);
            attackPanel.SetActive(false);
            attackUIMode = false;
        }
        else if(attackRangeMode)
        {
            attackPanel.SetActive(true);
            attackUIMode = true;
            attackRangeMode = false;
        }
        else if(moveMode)
        {
            moveMode = false;
            turnPanel.SetActive(true);
            ToggleMoveArea();
        }     
        count = 0;
        ClearAttackArea();
    }

    //버튼용
    public void ActiveAttackUI()
    {
        attackUIMode = true;
        attackPanel.SetActive(true);
        turnPanel.SetActive(false);
    }

    public void ActiveAttackArea()
    {
        if (actionPoint < myData._charAtd[attackNum].useAP) return;
        attackUIMode = false;
        attackRangeMode = true;
        count = 0;
        attackPanel.SetActive(false);
        turnPanel.SetActive(false);
    }

    private void GoToTurnUI()
    {
        ClearAttackArea();
        count = 0;
        attackUIMode = false;
        attackRangeMode = false;
        moveMode = false;
        attackPanel.SetActive(false);
        turnPanel.SetActive(true);
    }
}
