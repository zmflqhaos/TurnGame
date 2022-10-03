using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//게임 전체를 관리하는 데이터
[Serializable]
public class GameData
{
	public List<CharactorData> _playersData;
	public List<ItemSO> _publicInv;
}


//캐릭터 하나를 관리하는 데이터
[Serializable]
public class CharactorData
{
	public string _name;
	public Stat _charStat;
	public List<ItemSO> _privateInv;
    public List<AttackSO> _charAtd;
}

//아이템 하나에 들어갈 데이터


//스텟을 보관하는 클래스
[Serializable]
public class Stat
{
    public static int STAT_MIN = 0;
    public static int STAT_MAX = 25;
    /// <summary>
    /// ㅁㄴ
    /// </summary>
    public int _vitStat;
    public int _mtlStat;//정신력
    public int _strStat;//힘
    public int _dexStat;//민첩
    public int _intStat;//지능
    public int _chaStat;//매력
}

[Serializable]
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