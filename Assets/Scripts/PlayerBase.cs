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


    public override void Start()
    {
        base.Start();
        myData = GameManager.Instance.CurrentGameData._playersData[0];
        attackPos = LookRotation.D;
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
        Move();
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

    #region 무브
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

    private void Move()
    {
        if (!Input.GetMouseButtonDown(0) || !moveMode) return;
        mousePos = GameManager.Instance.mainCam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
        mousePosInt = Vector3Int.RoundToInt(mousePos);
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
        if (myData._charAtd[num].isSplash) target = new BattleBase[myData._charAtd[num].splashCount];
        AttackAreaRotation();
    }

    private void AttackAreaRotation()
    {
        Vector3Int vec = new Vector3Int();
        switch (attackPos)
        {
            case LookRotation.W:
                for (int i = 0; i < myData._charAtd[attackNum].attackRange.Count; i++)
                {
                    vec.Set(-myData._charAtd[attackNum].attackRange[i].y, myData._charAtd[attackNum].attackRange[i].x, 0);
                    attackAreas[i].transform.position = gameObject.transform.position + vec;
                    attackAreas[i].SetActive(true);
                }
                break;
            case LookRotation.A:
                for (int i = 0; i < myData._charAtd[attackNum].attackRange.Count; i++)
                {
                    vec.Set(-myData._charAtd[attackNum].attackRange[i].x, -myData._charAtd[attackNum].attackRange[i].y, 0);
                    attackAreas[i].transform.position = gameObject.transform.position + vec;
                    attackAreas[i].SetActive(true);
                }
                break;
            case LookRotation.S:
                for (int i = 0; i < myData._charAtd[attackNum].attackRange.Count; i++)
                {
                    vec.Set(myData._charAtd[attackNum].attackRange[i].y, -myData._charAtd[attackNum].attackRange[i].x, 0);
                    attackAreas[i].transform.position = gameObject.transform.position + vec;
                    attackAreas[i].SetActive(true);
                }
                break;
            case LookRotation.D:
                for (int i = 0; i < myData._charAtd[attackNum].attackRange.Count; i++)
                {
                    vec.Set(myData._charAtd[attackNum].attackRange[i].x, myData._charAtd[attackNum].attackRange[i].y, 0);
                    attackAreas[i].transform.position = gameObject.transform.position + vec;
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
        mousePos = GameManager.Instance.mainCam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
        mousePosInt = Vector3Int.RoundToInt(mousePos);

        if(myData._charAtd[attackNum].isSplash)
        {
            for (int i = 0; i < attackAreas.Length; i++)
            {
                if (!attackAreas[i].activeSelf) break;

                if (attackAreas[i].transform.position == mousePosInt)
                {
                    MapManager.Instance.SelectTile(mousePosInt)?.onTile.Hit(myData._charAtd[attackNum].damage, myData._charAtd[attackNum].damageType);
                    GoToMoveUI();
                }
            }
        }
        else
        {
            for (int i = 0; i < attackAreas.Length; i++)
            {
                if (!attackAreas[i].activeSelf) break;

                if (attackAreas[i].transform.position == mousePosInt)
                {
                    MapManager.Instance.SelectTile(mousePosInt)?.onTile.Hit(myData._charAtd[attackNum].damage, myData._charAtd[attackNum].damageType);
                    GoToMoveUI();
                }
            }
        }
    }

    #endregion

    public override void Hit(float damage, AttackCategory damageType)
    {
        base.Hit(damage, damageType);
    }

    public void MyTurn()
    {
        actionPoint = maxActionPoint;
        maxActionPoint = 3;
    }

    //버튼용
    public void ToggleMove()
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
        if (actionPoint < myData._charAtd[attackNum].useAP) return;
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
