using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public TileCategory tileCategory;
    public BattleBase onTile = null;

    public BattleBase CheckOnTile()
    {
        return onTile;
    }

    public void OnTileChange(BattleBase battle)
    {
        onTile = battle;
    }
    public void OnTileChange()
    {
        onTile = null;
    }
}
