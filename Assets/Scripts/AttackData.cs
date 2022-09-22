using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/AttackData")]
public class AttackData : ScriptableObject
{
    public List<Vector3Int> attackRange;//���ݹ���
    public float damage; //������
    public AttackCate damageType; //������ ������ ����

    public int useAP; //����ϴ� �ൿ��

    public bool isSplash; //���������ΰ�
    public int splashCount; //���� ���� ���� ����

}
