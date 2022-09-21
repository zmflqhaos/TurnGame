using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
 * ���⿡ �� �������ұ�
 * 5. �� �༮�� ������ �� ������ �� �ִ� ĭ�� ǥ�����ִ� ��Ÿ��� �־���ϴµ� ��� �ڷ����� �����ؾ��ϳ��ʹ�.
 * 6. ������ ����Ǿ��ִ� �����߿� �ϳ� ������� �Ǵ°Ű�, ������ ������ ������ �����ϴ� ���𰡰� �ʿ���
 * �ϴ� �������� �ϰ� ���Ŀ� �߰��ϱ�.
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
