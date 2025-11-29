using System;
using Events.ScripctsObject;
using UnityEngine;

namespace GameState
{
    public class GameBegin : MonoBehaviour
    {
        public VoidEventSO gameBeginEvent;

        public void Awake()
        {
            gameBeginEvent.RaiseEvent(this);
            Debug.Log("Game Begin Game Started");
        }
    }
}