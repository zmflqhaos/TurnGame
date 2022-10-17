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
    //�ּ�, �ִ뽺��
    public static int STAT_MIN = 0;
    public static int STAT_MAX = 25;

    /// <summary>
    /// ü��
    /// </summary>
    public int _vitStat;
    /// <summary>
    /// ���ŷ�
    /// </summary>
    public int _mtlStat;
    /// <summary>
    /// ��
    /// </summary>
    public int _strStat;
    /// <summary>
    /// ��ø
    /// </summary>
    public int _dexStat;
    /// <summary>
    /// ����
    /// </summary>
    public int _intStat;
    /// <summary>
    /// �ŷ�
    /// </summary>
    public int _chaStat;
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