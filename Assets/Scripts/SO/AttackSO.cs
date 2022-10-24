using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Attack")]
public class AttackSO : ScriptableObject
{
    public List<Vector3Int> attackRange;//���ݹ���
    public float damage; //������
    public AttackCategory damageType; //������ ������ ����

    public int useAP; //����ϴ� �ൿ��

    public bool isSplash; //���������ΰ�
    public int splashCount; //���� ���� ���� ����

}
