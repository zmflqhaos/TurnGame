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

    private LookRotation attackPos;
    private int attackNum;

    private Vector3 mousePos;
    private Vector3Int mousePosInt;

    private int count;

    public override void Start()
    {
        myData = GameManager.Instance.CurrentGameData.playersData[0];
        base.Start();
        hp = myData.hp;
        mental = myData.mental;
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
        Attack();
    }

    private void InputMouse()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        mousePos = GameManager.Instance.mainCam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
        mousePosInt = Vector3Int.RoundToInt(mousePos);
        if (attackArea.activeSelf) SelectTarget();
        if (moveArea.activeSelf) Move();
    }

    #region 무브
    public void ToggleMoveArea()
    {
        if (actionPoint <= 0) return;
        moveArea.SetActive(!moveArea.activeSelf);
        for (int i = 0; i < 4; i++)
            moveAreas[i].SetActive(CheckOutLine(moveAreas[i].transform.position) && moveArea.activeSelf);
    }

    private void Move()
    {
        Vector3 movePosition;
        Tile changeTile = null;
        for (int i = 0; i < 4; i++)
        {
            movePosition = onTile.transform.position + MapManager.Instance.vecOne[i];
            if (mousePosInt == movePosition && CheckOutLine(movePosition))
            {
                changeTile = MapManager.Instance.SelectTile(onTile.transform.position + MapManager.Instance.vecOne[i]);
                if (changeTile.onTile != null) break;
                onTile.OnTileChange();
                onTile = changeTile;
                onTile.OnTileChange(this);

                gameObject.transform.position = onTile.transform.position;
                ToggleMoveArea();
                actionPoint--;
                break;
            }
        }
    }

    #endregion

    #region 어택 UI
    private void AttackPosChange()
    {
        if (actionPoint < attacks[attackNum].useAP) return;
        if (attackArea.activeSelf)
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
        if (actionPoint < attacks[num].useAP) return;
        target = new BattleBase[attacks[num].splashCount];
        if (moveArea.activeSelf) ToggleMoveArea();
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
        for (int i = 0; i < attacks[attackNum].attackRange.Count; i++)
        {
            if(isXfirst)
            vec.Set((attacks[attackNum].attackRange[i].x * x), (attacks[attackNum].attackRange[i].y * y), 0);
            else
            vec.Set((attacks[attackNum].attackRange[i].y * y), (attacks[attackNum].attackRange[i].x * x), 0);
            attackAreas[i].transform.position = gameObject.transform.position + vec;
            attackAreas[i].SetActive(CheckOutLine(attackAreas[i].transform.position));
        }
    }

    public void ClearAttackArea()
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
            if (attackAreas[i].activeSelf && attackAreas[i].transform.position == mousePosInt)
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
            a?.Hit(SetFinalDamage(attackNum), attacks[attackNum].damageType, attacks[attackNum].attackType);
        }
        actionPoint -= attacks[attackNum].useAP;
        GoToTurnUI();
    }

    #endregion

    private bool CheckOutLine(Vector3 position)
    {
        return position.x >= 0
            && position.x <= MapManager.Instance.mapTiles.GetLength(1)-1
            && position.y >= 0
            && position.y <= MapManager.Instance.mapTiles.GetLength(0)-1;
    }

    public override void Hit(float damage, AttackCategory damageType, AttackType attackType)
    {
        base.Hit(damage, damageType, attackType);
    }

    private void GoToTurnUI()
    {
        ClearAttackArea();
        count = 0;
    }

    //버튼용
    public void MyTurn()
    {
        actionPoint = maxActionPoint;
        maxActionPoint = 3;
    }
}
