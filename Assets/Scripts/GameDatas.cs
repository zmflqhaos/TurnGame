using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Reflection;


//게임 전체를 관리하는 데이터
[System.Serializable]
public class GameData
{
	public List<PlayerData> _playersData;
	public List<ItemData> _publicInv;
}


//플레이어 한명을 관리하는 데이터
[System.Serializable]
public class PlayerData
{
	public string _name;
	public Stat _playerStat;
	public List<ItemData> _privateInv;
}


//아이템 하나에 들어갈 데이터
[CreateAssetMenu(menuName = "SO/ItemData")]
public class ItemData : ScriptableObject
{
	public string _name;
	public Category _category;
}


//스텟을 보관하는 클래스
[System.Serializable]
public class Stat
{
    public static int STAT_MIN = 0;
    public static int STAT_MAX = 25;

    public int _vitStat;
    public int _mtlStat;
    public int _strStat;
    public int _dexStat;
    public int _intStat;
    public int _chaStat;
}

[System.Serializable]
public class IntVec2
{
    public int x;
    public int y;
}

//아이템의 종류를 구분하는 이넘
public enum Category
{
    WEAPON,
    ARMOR,
    USE,
    QUEST,
    ETC
}

public enum AttackCate
{
    Physical,
    Magic,
    True,
}

public enum LookRotation
{
    W,
    A,
    S,
    D
}