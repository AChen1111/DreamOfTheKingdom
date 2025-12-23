using System;
using Manager;
using UnityEngine;

namespace GameState
{
    public class ResetRoomStart : MonoBehaviour
    {
        private void Awake()
        {
            UIManager.Instance.OnResetRoomEnter();
        }
    }
}
