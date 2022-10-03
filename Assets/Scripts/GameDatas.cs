using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//���� ��ü�� �����ϴ� ������
[Serializable]
public class GameData
{
	public List<CharactorData> _playersData;
	public List<ItemSO> _publicInv;
}


//ĳ���� �ϳ��� �����ϴ� ������
[Serializable]
public class CharactorData
{
	public string _name;
	public Stat _charStat;
	public List<ItemSO> _privateInv;
    public List<AttackSO> _charAtd;
}

//������ �ϳ��� �� ������


//������ �����ϴ� Ŭ����
[Serializable]
public class Stat
{
    public static int STAT_MIN = 0;
    public static int STAT_MAX = 25;
    /// <summary>
    /// ����
    /// </summary>
    public int _vitStat;
    public int _mtlStat;//���ŷ�
    public int _strStat;//��
    public int _dexStat;//��ø
    public int _intStat;//����
    public int _chaStat;//�ŷ�
}

[Serializable]
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