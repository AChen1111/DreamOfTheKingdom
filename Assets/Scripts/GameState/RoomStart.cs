using System;
using Manager;
using UnityEngine;

namespace GameState
{
    public class RoomStart : MonoBehaviour
    {
        private void Awake()
        {
            UIManager.Instance.OnGameWin();
        }
    }
}