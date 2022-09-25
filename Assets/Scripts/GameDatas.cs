using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Reflection;


//���� ��ü�� �����ϴ� ������
[System.Serializable]
public class GameData
{
	public List<PlayerData> _playersData;
	public List<ItemData> _publicInv;
}


//�÷��̾� �Ѹ��� �����ϴ� ������
[System.Serializable]
public class PlayerData
{
	public string _name;
	public Stat _playerStat;
	public List<ItemData> _privateInv;
}


//������ �ϳ��� �� ������
[CreateAssetMenu(menuName = "SO/ItemData")]
public class ItemData : ScriptableObject
{
	public string _name;
	public Category _category;
}


//������ �����ϴ� Ŭ����
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

//�������� ������ �����ϴ� �̳�
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