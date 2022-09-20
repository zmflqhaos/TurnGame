using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
 * 여기에 뭘 만들어야할까
 * 1. 현재 활성화 되어있는 타일맵의 사이즈를 들고와서 그에 맞춰 자신의 사이즈를 바꾸는거 (완료
 * 근데 카메라 사이즈만 변경하는게 더 쉽지 않을까? 
 *  L 카메라 사이즈를 바꾸면 UI까지 같이 바꿔야하고 무슨 문제가 더 생길지 모름 그냥 타일맵이랑 캐릭터 크기만 바꾸자
 * 1-2. 2번 하기 전에 얘 위치를 타일에 맞춰야해 - 타일맵을 활용할 수 있는 방법을 찾자. 아님 그냥 간격 계산해서 때려맞추던지
 * 2. UI에서 버튼 눌리면 타일맵에 얘 기준으로 상하좌우 4방향을 누를 수 있을 것 처럼 표시되는거 만들어야하고,
 * 3. 누르면 그쪽으로 이동하는거 만들어야함
 * 4. 팀 구분하는 bool 변수가 필요해
 * 5. 이 녀석이 공격할 때 공격할 수 있는 칸을 표시해주는 사거리가 있어야하는데 얘는 자료형을 뭐로해야하나싶다.
 * 6. 스텟은 저장되어있는 스텟중에 하나 때어오면 되는거고, 어떤 스텟을 들고올지 구분하는 무언가가 필요해
 * 7. 행동력을 관리해주는게 필요하고,
 * 8. 자신의 턴임을 알려주는게 필요해
 * 일단 이정도만 하고 이후에 추가하기.
*/

public class BattleChar : MonoBehaviour
{
    [SerializeField]
    private Tilemap[] mapList;
    private TileBase playerTile;
    private int mapNumber;
    private Vector3Int playerPos = new Vector3Int(0, 0, 0);

    private Vector3Int[] a = { new Vector3Int(1, 0, 0), new Vector3Int(0, 1, 0) , new Vector3Int(-1, 0, 0) , new Vector3Int(0, -1, 0) };

    void Start()
    {
        for(int i=0; i<mapList.Length; i++)
        {
            if(mapList[i].gameObject.activeSelf)
            {
                gameObject.transform.localScale = mapList[i].transform.localScale;
                gameObject.transform.position = mapList[i].GetCellCenterWorld(playerPos);
                playerTile = mapList[i].GetTile(playerPos);
                mapNumber = i;
                break;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if(mapList[mapNumber].GetTile(playerPos + a[1]))
                playerPos += a[1];
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (mapList[mapNumber].GetTile(playerPos + a[2]))
                playerPos += a[2];
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (mapList[mapNumber].GetTile(playerPos + a[3]))
                playerPos += a[3];
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (mapList[mapNumber].GetTile(playerPos + a[0]))
                playerPos += a[0];
        }
        gameObject.transform.position = mapList[mapNumber].GetCellCenterWorld(playerPos);
    }
}
