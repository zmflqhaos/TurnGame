using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//게임 전체를 관리하는 데이터
[Serializable]
public class GameData
{
	public List<CharactorData> playersData;
	public List<ItemSO> publicInv;
}

//캐릭터 하나를 관리하는 데이터
[Serializable]
public class CharactorData
{
	public string name;
    public int hp;
    public int mental;
	public Stat charStat;
	public List<int> privateInv;
    public List<int> charAtd;
}

//스텟을 보관하는 클래스
[Serializable]
public class Stat
{
    //최소, 최대스텟
    public static int STAT_MIN = 0;
    public static int STAT_MAX = 25;

    /// <summary>
    /// 체력
    /// </summary>
    public int vitStat;

    /// <summary>
    /// 정신력
    /// </summary>
    public int mtlStat;

    /// <summary>
    /// 힘
    /// </summary>
    public int strStat;

    /// <summary>
    /// 민첩
    /// </summary>
    public int dexStat;

    /// <summary>
    /// 지능
    /// </summary>
    public int intStat;

    /// <summary>
    /// 매력
    /// </summary>
    public int chaStat;
}

//아이템의 종류를 구분하는 이넘
public enum ItemCategory
{
    WEAPON,
    ARMOR,
    USE,
    QUEST,
    ETC
}

//공격의 데미지 타입을 결정함
public enum AttackCategory
{
    Physical,
    Magic,
    True,
}

//공격 방법을 결정함
public enum AttackType
{
    Melee, 
    Ranged
}

//바라보는 방향
public enum LookRotation
{
    W,
    A,
    S,
    D
}

//타일의 종류
public enum TileCategory
{
    Grass,
    Water,
    Hill,
}