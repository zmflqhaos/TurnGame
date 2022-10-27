using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIManager : MonoSingleton<BattleUIManager>
{
    [SerializeField]
    private GameObject turnPanel;
    [SerializeField]
    private GameObject attackPanel;
    [SerializeField]
    private PlayerBase player;

    void Update()
    {
        Tab();
    }

    public void Tab()
    {
        if (!Input.GetKeyDown(KeyCode.Tab)) return;

        if (attackPanel.activeSelf)
        {
            turnPanel.SetActive(true);
            attackPanel.SetActive(false);
            player.ClearAttackArea();
        }
    }

    //버튼용
    public void ActiveAttackUI()
    {
        attackPanel.SetActive(true);
        turnPanel.SetActive(false);
    }

    //버튼용
    public void ToggleMove()
    {
        player.ToggleMoveArea();
    }
}
