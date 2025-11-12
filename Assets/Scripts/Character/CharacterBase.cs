using UnityEngine;

public class CharacterBase : MonoBehaviour {
    public bool isDead = false;
    public int maxHp;
    public IntVariable hp;

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
    }
    public virtual void TakeDamage(int damage)
    {
        if (isDead) return;
        CurrentHP -= damage;
        if (CurrentHP <= 0)
        {
            CurrentHP = 0;
            isDead = true;
            animator.SetBool("isDead", true);
        }
    }
}