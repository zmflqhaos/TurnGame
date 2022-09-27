using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
 * 여기에 뭘 만들어야할까
 * 6. 스텟은 저장되어있는 스텟중에 하나 때어오면 되는거고, 누구의 스텟을 들고올지 구분하는 무언가가 필요해
 * 일단 이정도만 하고 이후에 추가하기.
*/

public class BattleChar : MonoBehaviour
{
    [SerializeField]
    private AttackData[] testData; //나중에 바꿔야해 제발

    private int myX;
    private int myY;

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

    private bool isPlayerTeam = true;
    private bool moveMode = false;
    private bool attackUIMode = false;
    private bool attackRangeMode = false;
    private int actionPoint;

    void Start()
    {  
        actionPoint = 3;
      
        myX = myY = 0;
        for(int i=0; i<4; i++)
        {
            moveAreas[i] = Instantiate(moveArea, gameObject.transform);
            moveAreas[i].name = "MoveArea";
            moveAreas[i].transform.position = BattleData.Instance.mapBox[myY][myX] + BattleData.Instance.vecOne[i];
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
            if(attackUIMode)
                ToggleAttackUI();
            if (attackRangeMode)
                ToggleAttackArea();
        }
        if(attackArea.activeSelf)
        {
            Attack();
        }
    }

    #region 무브
    private void ToggleMoveArea()
    {
        moveArea.SetActive(moveMode);
        if(moveMode)
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
        mousePos = BattleData.Instance.mainCam.ScreenToWorldPoint(Input.mousePosition)+new Vector3(0, 0, 10);
        mousePosInt = Vector3Int.RoundToInt(mousePos);
        for (int i = 0; i < 4; i++)
        {
            if (mousePosInt == BattleData.Instance.mapBox[myY][myX] + BattleData.Instance.vecOne[i] && CheckOutLine(i))
            {
                myX += BattleData.Instance.vecOne[i].x;
                myY += BattleData.Instance.vecOne[i].y;
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

    #region 어택 UI
    private void AttackPosChange()
    {
        if (!attackUIMode&&attackRangeMode)
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
        ClearAttackArea();
        AttackAreaRotation();
    }

    private void AttackAreaRotation()
    {
        Vector3Int vec = new Vector3Int();
        switch (attackPos)
        {
            case LookRotation.W :
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

        for(int i=0; i<attackAreas.Length; i++)
        {
            if (!attackAreas[i].activeSelf)
            {
                Debug.Log($"{i}번에서 탈출!");
                break;
            }
            if (mousePosInt == attackAreas[i].transform.position&&CheckAttackOutLine(i))
            {
                Debug.Log($"{mousePosInt} 위치의 적 공격!");
                Debug.Log($"{testData[attackNum].damage}의 데미지!");
                actionPoint -= testData[attackNum].useAP;
                attackRangeMode = false;
                GoToMoveUI();
                break;
            }
        }
    }

    private bool CheckAttackOutLine(int i)
    {
        float x = attackAreas[i].transform.position.x;
        float y = attackAreas[i].transform.position.y;
        return x >= 0 && x < BattleData.Instance.mapSize.x && y >= 0 && y < BattleData.Instance.mapSize.y;
    }

    #endregion

    public void MyTurn()
    {
        actionPoint = 3;
    }

    public void Move()
    {
        moveMode = !moveMode;
        ToggleMoveArea();
    }

    public void ToggleAttackUI()
    {
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
        attackUIMode = !attackUIMode;
        attackRangeMode = !attackRangeMode;
        attackPanel.SetActive(attackUIMode);
        ClearAttackArea();
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
