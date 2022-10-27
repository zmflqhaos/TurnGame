using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//���� ��ü�� �����ϴ� ������
[Serializable]
public class GameData
{
	public List<CharactorData> playersData;
	public List<ItemSO> publicInv;
}

//ĳ���� �ϳ��� �����ϴ� ������
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
    public int vitStat;

    /// <summary>
    /// ���ŷ�
    /// </summary>
    public int mtlStat;

    /// <summary>
    /// ��
    /// </summary>
    public int strStat;

    /// <summary>
    /// ��ø
    /// </summary>
    public int dexStat;

    /// <summary>
    /// ����
    /// </summary>
    public int intStat;

    /// <summary>
    /// �ŷ�
    /// </summary>
    public int chaStat;
}

//�������� ������ �����ϴ� �̳�
public enum ItemCategory
{
    WEAPON,
    ARMOR,
    USE,
    QUEST,
    ETC
}

//������ ������ Ÿ���� ������
public enum AttackCategory
{
    Physical,
    Magic,
    True,
}

//���� ����� ������
public enum AttackType
{
    Melee, 
    Ranged
}

//�ٶ󺸴� ����
public enum LookRotation
{
    W,
    A,
    S,
    D
}

//Ÿ���� ����
public enum TileCategory
{
    Grass,
    Water,
    Hill,
}