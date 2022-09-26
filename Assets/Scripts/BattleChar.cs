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
    private float mapScale;

    [SerializeField]
    private IntVec2[] mapSizes;
    private IntVec2 mapSize;
    private List<List<Vector3Int>> mapBox = new List<List<Vector3Int>>();
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
        for(int i=0; i<mapSize.y; i++)
        {
            mapBox.Add(new List<Vector3Int>());
            for(int j=0; j<mapSize.x; j++)
            {
                mapBox[i].Add(new Vector3Int(j, i));
            }
        }
        myX = myY = 0;
        for(int i=0; i<4; i++)
        {
            moveAreas[i] = Instantiate(moveArea, gameObject.transform);
            moveAreas[i].name = "MoveArea";
            moveAreas[i].transform.position = mapBox[myY][myX] + a[i];
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

    #region 무브
    private void ToggleMoveArea()
    {
        moveArea.SetActive(moveMode);
        for (int i = 0; i < 4; i++)
            moveAreas[i].SetActive(moveMode);
    }

    private void AreaClickMove()
    {
        if (!Input.GetMouseButtonDown(0) || !moveMode) return;

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition)+new Vector3(0, 0, 10);
        mousePosInt = Vector3Int.RoundToInt(mousePos);
        Debug.Log(mousePosInt);
        for (int i = 0; i < 4; i++)
        {   //-1 0 0                0 0 0       -1
            if (mousePosInt == mapBox[myY][myX]+a[i]) //i=1
            {
                gameObject.transform.position = mapBox[myY][myX];
                actionPoint--;
                moveMode = false;
                break;
            }
        }
        ToggleMoveArea();
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
                    attackAreas[i].transform.position = mapBox[myY][myX] + vec;
                    attackAreas[i].SetActive(true);
                }
                break;
            case LookRotation.A:
                for (int i = 0; i < testData[attackNum].attackRange.Count; i++)
                {
                    vec.Set(-testData[attackNum].attackRange[i].x, -testData[attackNum].attackRange[i].y, 0);
                    attackAreas[i].transform.position = mapBox[myY][myX] + vec;
                    attackAreas[i].SetActive(true);
                }
                break;
            case LookRotation.S:
                for (int i = 0; i < testData[attackNum].attackRange.Count; i++)
                {
                    vec.Set(testData[attackNum].attackRange[i].y, -testData[attackNum].attackRange[i].x, 0);
                    attackAreas[i].transform.position = mapBox[myY][myX] + vec;
                    attackAreas[i].SetActive(true);
                }
                break;
            case LookRotation.D:
                for (int i = 0; i < testData[attackNum].attackRange.Count; i++)
                {
                    vec.Set(testData[attackNum].attackRange[i].x, testData[attackNum].attackRange[i].y, 0);
                    attackAreas[i].transform.position = mapBox[myY][myX] + vec;
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
    #endregion

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
