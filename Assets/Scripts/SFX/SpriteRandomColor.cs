using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 实现颜色随机变化
/// </summary>
public class SpriteRandomColor : MonoBehaviour
{
    private SpriteRenderer _sr;
    public float cycleDuration = 3f;  // 完成一圈的时间（秒）
    [Range(0,1)] public float saturation = 1f; //饱和度
    [Range(0, 1)] public float value = 1f; //亮度

    private Tween _t;

    private void Awake()
    {
        if (!_sr)
        {
            _sr = GetComponent<SpriteRenderer>();
        }
    }
    void OnEnable()
    {
        _t = DOVirtual.Float(0f, 1f, cycleDuration, h =>
        {
            Color c = Color.HSVToRGB(h, saturation, value);
            c.a = _sr.color.a;
            _sr.color = c;
        }
        ).SetEase(Ease.Linear)
        .SetLoops(-1,LoopType.Yoyo)
        .SetLink(gameObject,LinkBehaviour.KillOnDisable);
    }
    private void OnDisable() {
        _t?.Kill();
        _t = null;
    }
}