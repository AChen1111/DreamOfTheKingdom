using Character.Enemy;
using Manager;
using UnityEngine;

public class WinBoss : MonoBehaviour
{
    public Enemy boss;
    public bool isFirstTime = true;
    void Update()
    {
        if (boss.isDead)
        {
            if (isFirstTime)
            {
                isFirstTime = false;
                UIManager.Instance.OnBossDead();
            }
           
        }
    }
}
