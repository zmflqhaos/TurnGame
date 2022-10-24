using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Attack")]
public class AttackSO : ScriptableObject
{
    public List<Vector3Int> attackRange;//공격범위
    public float damage; //데미지
    public AttackCategory damageType; //공격의 데미지 종류

    public int useAP; //사용하는 행동력

    public bool isSplash; //광역공격인가
    public int splashCount; //동시 공격 가능 개수

}
