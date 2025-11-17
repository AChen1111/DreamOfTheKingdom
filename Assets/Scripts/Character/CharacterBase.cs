using System.Collections;
using UnityEngine;

public class CharacterBase : MonoBehaviour {
    public bool isDead = false;
    public int maxHp;
    public IntVariable hp;
    public IntVariable defense;
    public GameObject buff;
    public GameObject deBuff;
    
    public int CurrentHP
    {
        get { return hp.currentValue; }
        set { hp.SetValue(value); }
    }
    
    public int MaxHp
    {
        get { return maxHp; }
    }
    
    private Animator animator;
    
    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    
    protected virtual void Start()
    {
        hp.maxValue = maxHp;
        CurrentHP = MaxHp;
        ResetDefense();
    }
    public virtual void TakeDamage(int damage)
    {
        if (isDead) return;
        
        //伤害够
        if (damage >= defense.currentValue)
        {
            CurrentHP -= (damage  - defense.currentValue);
            defense.SetValue(0);
        }
        //伤害不够
        else
        {
            defense.SetValue
                (defense.currentValue - damage);
        }

        if (CurrentHP <= 0)
        {
            CurrentHP = 0;
            isDead = true;
            animator.SetBool("isDead", true);
        }
    }
    
    /// <summary>
    /// 更新防御值(外部调用)
    /// </summary>
    /// <param name="value"></param>
    public void UpdateDefense(int value)
    {
        var cur = defense.currentValue + value;
        defense.SetValue(cur);
    }
    
    /// <summary>
    /// 重置防御值 (玩家脚本监听 玩家回合开始)
    /// </summary>
    public void ResetDefense()
    {
        defense.SetValue(0);
    }
    
    /// <summary>
    /// 回血 对外接口
    /// </summary>
    /// <param name="value"></param>
    public void HealHealth(int value)
    {
        CurrentHP += value;
        CurrentHP = Mathf.Min(CurrentHP,maxHp);
        StartCoroutine(doBuffAnimation());
    }
    
    
    /// <summary>
    /// 播放buff动画
    /// </summary>
    /// <returns></returns>
    IEnumerator doBuffAnimation()
    {
        buff.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        buff.SetActive(false);
    }
    
    /// <summary>
    /// 播放debuff动画
    /// </summary>
    /// <returns></returns>
    IEnumerator doDeBuffAnimation()
    {
        deBuff.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        deBuff.SetActive(false);
    }
}