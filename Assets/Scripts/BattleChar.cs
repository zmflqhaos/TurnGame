using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
 * 여기에 뭘 만들어야할까
 * 5. 이 녀석이 공격할 때 공격할 수 있는 칸을 표시해주는 사거리가 있어야하는데 얘는 자료형을 뭐로해야하나싶다.
 * 6. 스텟은 저장되어있는 스텟중에 하나 때어오면 되는거고, 누구의 스텟을 들고올지 구분하는 무언가가 필요해
 * 일단 이정도만 하고 이후에 추가하기.
*/

public class BattleChar : MonoBehaviour
{
    private Camera mainCam;
    private Vector3Int[] a = { new Vector3Int(0, 1, 0), new Vector3Int(-1, 0, 0), new Vector3Int(0, -1, 0), new Vector3Int(1, 0, 0) };

    [SerializeField]
    private Tilemap[] mapList;
    private int mapNumber;

    private GameObject[] moveAreas = new GameObject[4];
    [SerializeField]
    private GameObject moveArea;

    private Vector3 mousePos;
    private Vector3Int mousePosInt;

    private Vector3Int playerPos = new Vector3Int(0, 0, 0);

    private bool isPlayerTeam = true;
    private bool moveMode = false;
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
        ActiveMoveArea();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)&&moveMode)
        {
            AreaClickMove();
        }
    }

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
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePosInt = mapList[mapNumber].WorldToCell(mousePos);
        for (int i=0; i<4; i++)
        {
            if (mousePosInt == playerPos+a[i])
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
}
