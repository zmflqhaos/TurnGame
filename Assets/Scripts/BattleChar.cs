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
    private Camera mainCam;
    private Vector3Int[] a = { new Vector3Int(0, 1, 0), new Vector3Int(-1, 0, 0), new Vector3Int(0, -1, 0), new Vector3Int(1, 0, 0) };

    [SerializeField]
    private AttackData[] testData; //나중에 바꿔야해 제발

    [SerializeField]
    private Tilemap[] mapList;
    private int mapNumber;

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

    private Vector3Int playerPos = new Vector3Int(0, 0, 0);

    private bool isPlayerTeam = true;
    private bool moveMode = false;
    private bool attackMode = false;
    private int actionPoint;

    void Start()
    {
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        actionPoint = 3;
        for (int i=0; i<mapList.Length; i++)
        {
            if(mapList[i].gameObject.activeSelf)
            {
                gameObject.transform.localScale = mapList[i].transform.localScale;
                gameObject.transform.position = mapList[i].GetCellCenterWorld(playerPos);
                mapNumber = i;
                break;
            }
        }
        for(int i=0; i<4; i++)
        {
            moveAreas[i] = Instantiate(moveArea, gameObject.transform);
            moveAreas[i].name = "MoveArea";
            moveAreas[i].transform.position = mapList[mapNumber].GetCellCenterWorld(playerPos + a[i]);
        }
        for (int i = 0; i < 10; i++)
        {
            attackAreas[i] = Instantiate(attackArea, gameObject.transform);
            attackAreas[i].name = "AttackArea";
        }
        ActiveMoveArea();
        ChangeAttackArea(-1);
    }

    void Update()
    {
        AreaClickMove();
        AttackPosChange();
    }

    #region 무브
    private void ActiveMoveArea()
    {
        moveArea.SetActive(moveMode);
        if (moveMode)
        {
            for (int i = 0; i < 4; i++)
                moveAreas[i].SetActive(mapList[mapNumber].GetTile(playerPos + a[i]));
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

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePosInt = mapList[mapNumber].WorldToCell(mousePos);
        for (int i = 0; i < 4; i++)
        {
            if (mousePosInt == playerPos + a[i])
            {
                if (mapList[mapNumber].GetTile(playerPos + a[i]))
                {
                    playerPos += a[i];
                    actionPoint--;
                    moveMode = false;
                }
                break;
            }
        }
        ActiveMoveArea();
        gameObject.transform.position = mapList[mapNumber].GetCellCenterWorld(playerPos);
    }
    #endregion

    #region 어택
    private void AttackPosChange()
    {
        if (attackMode)
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
        attackArea.SetActive(attackMode);

        for (int i = 0; i < 10; i++)
            attackAreas[i].SetActive(false);

        if (num == -1) return;

        if (attackMode)
        {
            attackNum = num;
            AttackAreaRotation();
        }
        else
        {
            for (int i = 0; i < 10; i++)
                attackAreas[i].SetActive(false);
        }
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
                    attackAreas[i].transform.position = mapList[mapNumber].GetCellCenterWorld(vec);
                    attackAreas[i].SetActive(true);
                }
                break;
            case LookRotation.A:
                for (int i = 0; i < testData[attackNum].attackRange.Count; i++)
                {
                    vec.Set(-testData[attackNum].attackRange[i].x, -testData[attackNum].attackRange[i].y, 0);
                    attackAreas[i].transform.position = mapList[mapNumber].GetCellCenterWorld(vec);
                    attackAreas[i].SetActive(true);
                }
                break;
            case LookRotation.S:
                for (int i = 0; i < testData[attackNum].attackRange.Count; i++)
                {
                    vec.Set(testData[attackNum].attackRange[i].y, -testData[attackNum].attackRange[i].x, 0);
                    attackAreas[i].transform.position = mapList[mapNumber].GetCellCenterWorld(vec);
                    attackAreas[i].SetActive(true);
                }
                break;
            case LookRotation.D:
                for (int i = 0; i < testData[attackNum].attackRange.Count; i++)
                {
                    vec.Set(testData[attackNum].attackRange[i].x, testData[attackNum].attackRange[i].y, 0);
                    attackAreas[i].transform.position = mapList[mapNumber].GetCellCenterWorld(vec);
                    attackAreas[i].SetActive(true);
                }
                break;
        }
    }
    #endregion

    public void MyTurn()
    {
        actionPoint = 3;
    }

    public void Move()
    {
        if (actionPoint <= 0) return;
        moveMode = !moveMode;
        ActiveMoveArea();
    }

    public void ShowAttackUI()
    {
        if (actionPoint <= 0) return;
        attackMode = !attackMode;
        attackPanel.SetActive(attackMode);
        turnPanel.SetActive(!attackMode);
    }
}
