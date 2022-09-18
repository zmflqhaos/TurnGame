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


//스텟에 관련한 함수가 있는 클래스
[System.Serializable]
public class Stat
{
    public Action OnStatChanged = null;

    public static int STAT_MIN = 0;
    public static int STAT_MAX = 25;


    private SingleStat _hpStat;
    private SingleStat _mpStat;
    private SingleStat _strStat;
    private SingleStat _dexStat;
    private SingleStat _intStat;
    private SingleStat _chaStat;

    public enum Type
    {
        VIT = 0,
        MGI = 1,
        STR = 2,
        DEX = 3,
        INT = 4,
        CHA = 5
    }

    public Stat(int hpAmount, int mpAmount, int strAmount, int dexAmount, int inteAmount, int chaAmount)
    {
        _hpStat = new SingleStat(hpAmount);
        _mpStat = new SingleStat(mpAmount);
        _strStat = new SingleStat(strAmount);
        _dexStat = new SingleStat(dexAmount);
        _intStat = new SingleStat(inteAmount);
        _chaStat = new SingleStat(chaAmount);
    }

    public int GetStatAmount(Type statType)
    {
        return GetSingleStat(statType).GetStatAmount();
    }

    private SingleStat GetSingleStat(Type statType)
    {
        String statFieldName = $"_{statType.ToString().ToLower()}Stat";
        FieldInfo fInfo = this.GetType().GetField(statFieldName, BindingFlags.Instance | BindingFlags.NonPublic);
        SingleStat s = fInfo.GetValue(this) as SingleStat;
        return s;
    }

    public void SetStatAmount(Type statType, int amount)
    {
        GetSingleStat(statType).SetStatAmount(amount);
        OnStatChanged?.Invoke();
    }

    public float GetStatAmountNormalized(Type statType)
    {
        return GetSingleStat(statType).GetStatAmountNomalize();
    }

    public void IncStatAmount(Type statType)
    {
        SetStatAmount(statType, GetStatAmount(statType) + 1);
    }

    public void DecStatAmount(Type statType)
    {
        SetStatAmount(statType, GetStatAmount(statType) - 1);
    }

    private class SingleStat
    {
        private int _stat;
        public SingleStat(int amount)
        {
            SetStatAmount(amount);
        }
        public void SetStatAmount(int amount)
        {
            _stat = Mathf.Clamp(amount, STAT_MIN, STAT_MAX);
        }

        public int GetStatAmount()
        {
            return _stat;
        }

        public float GetStatAmountNomalize()
        {
            return (float)_stat / STAT_MAX;
        }
    }
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
