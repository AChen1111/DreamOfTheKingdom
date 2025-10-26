using UnityEngine;

public class Line : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    [Header("线滚动速度")]
    public float lineSpeed = 2f;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    void Update()
    {
        if(_lineRenderer != null)
        {
            var offset = _lineRenderer.material.mainTextureOffset;
            offset.x += Time.deltaTime * lineSpeed;
            _lineRenderer.material.mainTextureOffset = offset;
        }
    }
}
