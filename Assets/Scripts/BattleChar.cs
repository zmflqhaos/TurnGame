using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
 * ���⿡ �� �������ұ�
 * 1. ���� Ȱ��ȭ �Ǿ��ִ� Ÿ�ϸ��� ����� ���ͼ� �׿� ���� �ڽ��� ����� �ٲٴ°� (�Ϸ�
 * �ٵ� ī�޶� ����� �����ϴ°� �� ���� ������? 
 *  L ī�޶� ����� �ٲٸ� UI���� ���� �ٲ���ϰ� ���� ������ �� ������ �� �׳� Ÿ�ϸ��̶� ĳ���� ũ�⸸ �ٲ���
 * 1-2. 2�� �ϱ� ���� �� ��ġ�� Ÿ�Ͽ� ������� - Ÿ�ϸ��� Ȱ���� �� �ִ� ����� ã��. �ƴ� �׳� ���� ����ؼ� �������ߴ���
 * 2. UI���� ��ư ������ Ÿ�ϸʿ� �� �������� �����¿� 4������ ���� �� ���� �� ó�� ǥ�õǴ°� �������ϰ�,
 * 3. ������ �������� �̵��ϴ°� ��������
 * 4. �� �����ϴ� bool ������ �ʿ���
 * 5. �� �༮�� ������ �� ������ �� �ִ� ĭ�� ǥ�����ִ� ��Ÿ��� �־���ϴµ� ��� �ڷ����� �����ؾ��ϳ��ʹ�.
 * 6. ������ ����Ǿ��ִ� �����߿� �ϳ� ������� �Ǵ°Ű�, � ������ ������ �����ϴ� ���𰡰� �ʿ���
 * 7. �ൿ���� �������ִ°� �ʿ��ϰ�,
 * 8. �ڽ��� ������ �˷��ִ°� �ʿ���
 * �ϴ� �������� �ϰ� ���Ŀ� �߰��ϱ�.
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
