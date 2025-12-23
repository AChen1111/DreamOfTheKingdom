using System;
using Card.ScripctsObject;
using Character;
using Events.ScripctsObject;
using Manager;
using Rooms.ScriptsObject;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace UI.Panel
{
    public class GameStartPanel : MonoBehaviour
    {
        public Button btnStart;
        public Button btnQuit;
        
        public MapLayoutSO map;
        public IntVariable PlayerHp;
        public Player player;
        public CardManager cardManager;
        
        [Header("事件广播")]
        public ObjectEventSO loadMapEvent;
        
        private void Reset()
        {
            btnStart = GameObject.Find("BtnStart").GetComponent<Button>();
            btnQuit = GameObject.Find("BtnQuit").GetComponent<Button>();
        }

        private void Awake()
        {
            
            btnStart.onClick.AddListener(() =>
                {
                    UIManager.Instance.OnMenuOver();
                    loadMapEvent.RaiseEvent(null,this);
                    map.ResetMapSO();
                    cardManager.loadCard();
                    PlayerHp.currentValue = PlayerHp.maxValue;
                    player.isDead = false;
                    Debug.Log("loaded");
                }
                );
            
            btnQuit.onClick.AddListener(Application.Quit);
        }
    }
}
