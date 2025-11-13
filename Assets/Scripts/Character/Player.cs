using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : CharacterBase
{
    public IntVariable playerMana;
    public int maxMana;
    public int CurrentMana
    {
        get => playerMana.currentValue;
        set => playerMana.SetValue(value);
    }

    private void OnEnable()
    {
        playerMana.maxValue  = maxMana;
        StartCoroutine(WaitForUiLoad());
        
    }
    
    /// <summary>
    /// 监听 回合开始事件
    /// </summary>
    public void NewTurn()
    {
        CurrentMana = maxMana;
    }
    
    /// <summary>
    /// 监听 卡牌打出事件
    /// </summary>
    /// <param name="cost"></param>
    public void UpdateMana(int cost)
    {
        CurrentMana -= cost;
        if (CurrentMana < 0)
        {
            CurrentMana = 0;
        }
    }

    IEnumerator WaitForUiLoad()
    {
        yield return new WaitForEndOfFrame();
        CurrentMana = playerMana.maxValue;
    }
}