using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
 * ���⿡ �� �������ұ�
 * 6. ������ ����Ǿ��ִ� �����߿� �ϳ� ������� �Ǵ°Ű�, ������ ������ ������ �����ϴ� ���𰡰� �ʿ���
 * �ϴ� �������� �ϰ� ���Ŀ� �߰��ϱ�.
*/

public class BattleChar : MonoBehaviour
{
    private Camera mainCam;
    private Vector3Int[] a = { new Vector3Int(0, 1, 0), new Vector3Int(-1, 0, 0), new Vector3Int(0, -1, 0), new Vector3Int(1, 0, 0) };

    [SerializeField]
    private AttackData[] testData; //���߿� �ٲ���� ����

    [SerializeField]
    private Tilemap[] mapList;
    private int mapNumber;
    private float mapScale;

    [SerializeField]
    private IntVec2[] mapSizes;
    private IntVec2 mapSize;
    private List<List<bool>> currentMapSize;

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
    private bool attackUIMode = false;
    private bool attackRangeMode = false;
    private int actionPoint;

    void Start()
    {
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        actionPoint = 3;
        for (int i=0; i<mapList.Length; i++)
        {
            if(mapList[i].gameObject.activeSelf)
            {
                mapScale = mapList[i].transform.localScale.x;
                mapNumber = i;
                mapSize = mapSizes[i];
                break;
            }
        }
        for(int i=0; i<mapSize.x; i++)
        {
            currentMapSize.Add(new List<bool>());
            for(int j=0; j<mapSize.y; j++)
            {
                currentMapSize[i].Add(false);
            }
        }
        gameObject.transform.localScale.Set(mapScale, mapScale, 1);
        //gameObject.transform.position = new Vector3Int(mapSize[mapNumber].x, mapSize[mapNumber].y);
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
            
    }

    #region ����
    private void ToggleMoveArea()
    {
        moveArea.SetActive(moveMode);
        if (moveMode)
        {
            for (int i = 0; i < 4; i++)
                moveAreas[i].SetActive(CheckUnder(playerPos + a[i]));
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
            if (mousePosInt == playerPos + a[i]&& CheckUnder(playerPos + a[i]))
            {
                playerPos += a[i];
                actionPoint--;
                moveMode = false;
                break;
            }
        }
        ToggleMoveArea();
        gameObject.transform.position = mapList[mapNumber].GetCellCenterWorld(playerPos);
    }
    #endregion

    #region ���� UI
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
                    attackAreas[i].transform.position = mapList[mapNumber].GetCellCenterWorld(vec + playerPos);
                    attackAreas[i].SetActive(CheckUnder(vec + playerPos));
                }
                break;
            case LookRotation.A:
                for (int i = 0; i < testData[attackNum].attackRange.Count; i++)
                {
                    vec.Set(-testData[attackNum].attackRange[i].x, -testData[attackNum].attackRange[i].y, 0);
                    attackAreas[i].transform.position = mapList[mapNumber].GetCellCenterWorld(vec + playerPos);
                    attackAreas[i].SetActive(CheckUnder(vec + playerPos));
                }
                break;
            case LookRotation.S:
                for (int i = 0; i < testData[attackNum].attackRange.Count; i++)
                {
                    vec.Set(testData[attackNum].attackRange[i].y, -testData[attackNum].attackRange[i].x, 0);
                    attackAreas[i].transform.position = mapList[mapNumber].GetCellCenterWorld(vec + playerPos);
                    attackAreas[i].SetActive(CheckUnder(vec + playerPos));
                }
                break;
            case LookRotation.D:
                for (int i = 0; i < testData[attackNum].attackRange.Count; i++)
                {
                    vec.Set(testData[attackNum].attackRange[i].x, testData[attackNum].attackRange[i].y, 0);
                    attackAreas[i].transform.position = mapList[mapNumber].GetCellCenterWorld(vec + playerPos);
                    attackAreas[i].SetActive(CheckUnder(vec + playerPos));
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
    #endregion
    public bool CheckUnder(Vector3Int pos)
    {
        return mapList[mapNumber].GetTile(pos);
    }

    public void MyTurn()
    {
        actionPoint = 3;
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
        attackUIMode = !attackUIMode;
        attackRangeMode = !attackRangeMode;
        attackPanel.SetActive(attackUIMode);
    }
}
