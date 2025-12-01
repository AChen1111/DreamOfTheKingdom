using System;
using Events.ScripctsObject;
using UnityEngine;

namespace GameState
{

    public class GameBegin : MonoBehaviour
    {
         public VoidEventSO gameBeginEvent;
        //
        public void Awake()
        {
            Debug.Log("GameBegin Awake");
            gameBeginEvent.RaiseEvent(this);
        }
    }
}