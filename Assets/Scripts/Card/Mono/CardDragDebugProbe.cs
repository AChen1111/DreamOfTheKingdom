// CardDragDebugProbe.cs
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; // GraphicRaycaster

public class CardDragDebugProbe : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Tooltip("敌人使用的 Tag")]
    public string enemyTag = "Enemy";

    [Tooltip("打印 RaycastAll 的完整命中栈")]
    public bool verbose = true;

    private CardDragHandler _handler;
    private Camera _cam;

    void Awake()
    {
        _handler = GetComponent<CardDragHandler>();
        _cam = Camera.main;

        if (EventSystem.current == null)
            Debug.LogError("[Probe] 场景里没有 EventSystem！(UI/EventSystem)");

        if (_cam != null)
        {
            var has3D = _cam.GetComponent<PhysicsRaycaster>() != null;
            var has2D = _cam.GetComponent<Physics2DRaycaster>() != null;
            Debug.Log($"[Probe] 摄像机 Raycaster -> Physics(3D)={has3D}, Physics2D={has2D}");
        }

        var canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            var gr = canvas.GetComponent<GraphicRaycaster>();
            Debug.Log($"[Probe] Canvas 上是否有 GraphicRaycaster：{(gr != null)}");
        }
    }

    public void OnBeginDrag(PointerEventData e) => Probe(e, "BeginDrag");
    public void OnDrag(PointerEventData e)      => Probe(e, "Drag");
    public void OnEndDrag(PointerEventData e)   => Probe(e, "EndDrag");

    private void Probe(PointerEventData e, string phase)
    {
        var pe = e.pointerEnter ? e.pointerEnter : null;
        var pcr = e.pointerCurrentRaycast.gameObject ? e.pointerCurrentRaycast.gameObject : null;

        Debug.Log($"[Probe:{phase}] pointerEnter={(pe?pe.name:"NULL")}, pointerCurrentRaycast={(pcr?pcr.name:"NULL")}");

        // 打印 UI/EventSystem 的完整命中栈
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(e, results);
        if (verbose)
        {
            for (int i = 0; i < results.Count; i++)
            {
                var go = results[i].gameObject;
                Debug.Log($"[Probe:{phase}] Hit[{i}] {Describe(go)}");
            }
        }

        // 以 Raycast 栈顶为候选（没有则退回 pointerEnter）
        var candidate = results.Count > 0 ? results[0].gameObject : pe;

        // 1) 检查是否打到了“卡牌自身”或其子物体（会挡住敌人）
        if (candidate != null && IsSelfOrChild(candidate.transform))
        {
            Debug.LogWarning($"[Probe:{phase}] Raycast 命中了卡牌自身/子物体，正挡住敌人。拖拽时请将卡牌/箭头改到 Ignore Raycast 图层或禁用其 Collider/UI Raycast。");
        }

        // 2) 检查箭头是否挡射线（需要反射 CardDragHandler 的 private 字段）
        var currentArrow = GetPrivateField<GameObject>(_handler, "currentArrow");
        if (currentArrow != null)
        {
            var layerName = LayerMask.LayerToName(currentArrow.layer);
            Debug.Log($"[Probe:{phase}] currentArrow = {currentArrow.name} (Layer={layerName})");
            if (layerName != "Ignore Raycast")
                Debug.LogWarning($"[Probe:{phase}] 箭头没有放在 Ignore Raycast 图层，可能挡住射线。");
        }

        // 3) 在父链中查找 CharacterBase（无需知道命名空间）
        var cbRoot = FindCharacterBaseRoot(candidate);
        if (cbRoot != null)
        {
            Debug.Log($"[Probe:{phase}] 在父链找到 CharacterBase：{cbRoot.name}, tag={cbRoot.tag}, layer={LayerMask.LayerToName(cbRoot.layer)}");
            if (!cbRoot.CompareTag(enemyTag))
                Debug.LogWarning($"[Probe:{phase}] 目标物体未打 '{enemyTag}' Tag（当前是 '{cbRoot.tag}'）。");
        }
        else
        {
            Debug.LogWarning($"[Probe:{phase}] 没在命中物体的父链里找到 CharacterBase（可能命中的是敌人的子节点/空白/错误物体）。");
        }

        // 4) 读出你脚本里的 canExecute/targetCharacter，确认失败原因
        bool canExecute = GetPrivateField<bool>(_handler, "canExecute");
        var targetCharacter = GetPrivateField<Object>(_handler, "targetCharacter");

        Debug.Log($"[Probe:{phase}] CardDragHandler: canExecute={canExecute}, targetCharacter={(targetCharacter?targetCharacter.name:"NULL")}");

        // 5) 给出可执行性的综合判断
        if (phase == "EndDrag")
        {
            if (!canExecute || targetCharacter == null)
                Debug.LogError($"[Probe:{phase}] 结束拖拽未触发 —— canExecute={canExecute}, targetCharacter={(targetCharacter?targetCharacter.name:"NULL")}；请对照上面警告定位（射线被挡 / Raycaster 缺失 / Tag 不匹配 / 组件在父节点等）。");
            else
                Debug.Log($"[Probe:{phase}] 条件满足：预计会调用 ExecuteEffect(from, {targetCharacter.name})。");
        }
    }

    // —— 工具方法 ——
    private static T GetPrivateField<T>(object obj, string name)
    {
        if (obj == null) return default;
        var f = obj.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
        return f != null ? (T)f.GetValue(obj) : default;
    }

    private static string Describe(GameObject go)
    {
        if (!go) return "NULL";
        return $"{go.name} (tag={go.tag}, layer={LayerMask.LayerToName(go.layer)})";
    }

    private static GameObject FindCharacterBaseRoot(GameObject start)
    {
        if (!start) return null;
        var t = start.transform;
        while (t != null)
        {
            var comps = t.GetComponents<Component>();
            foreach (var c in comps)
            {
                if (c == null) continue;
                if (c.GetType().Name == "CharacterBase")
                    return t.gameObject;
            }
            t = t.parent;
        }
        return null;
    }

    private bool IsSelfOrChild(Transform t)
    {
        var self = transform;
        while (t != null)
        {
            if (t == self) return true;
            t = t.parent;
        }
        return false;
    }
}
