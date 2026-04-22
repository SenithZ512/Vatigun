using UnityEngine;
using DG.Tweening;

public class SlidingDoor : MonoBehaviour
{
    [Header("Door Transform")]
    public Transform door;  // ลาก object บานประตูมาใส่ตรงนี้

    [Header("Slide")]
    public float slideDistance = 2f;
    public Vector3 slideAxis = Vector3.right; // X = เลื่อนซ้าย/ขวา, Z = เลื่อนเข้า/ออก

    [Header("Animation")]
    public float openDuration = 0.6f;
    public float closeDuration = 0.5f;
    public Ease openEase = Ease.OutCubic;
    public Ease closeEase = Ease.InCubic;

    [Header("Proximity Detection")]
    public float openRadius = 3f;  // ระยะที่ประตูเปิด
    public float closeRadius = 4f;  // ระยะที่ประตูปิด (ควรมากกว่า openRadius)
    public Transform player;        // ปล่อยว่างไว้ได้ จะ auto-find จาก Tag "Player"

    [Header("Close Delay")]
    public float closeDelay = 0.5f;

    // --- Private ---
    private Vector3 _closedPos;
    private Vector3 _openPos;
    private bool _isOpen = false;
    private bool _isAnimating = false;
    private Tween _closeTween;

    void Start()
    {
        _closedPos = door.localPosition;
        _openPos = _closedPos + slideAxis.normalized * slideDistance;

        if (player == null)
        {
            GameObject go = GameObject.FindWithTag("Player");
            if (go != null) player = go.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (!_isOpen && dist <= openRadius)
            Open();
        else if (_isOpen && dist > closeRadius)
            ScheduleClose();
    }

    // ========== DOOR CONTROL ==========

    public void Open()
    {
        if (_isOpen) return;
        _isOpen = true;
        _closeTween?.Kill();
        _isAnimating = true;

        door.DOLocalMove(_openPos, openDuration)
            .SetEase(openEase)
            .OnComplete(() => _isAnimating = false);
    }

    public void Close()
    {
        if (!_isOpen) return;
        _isOpen = false;
        _isAnimating = true;

        door.DOLocalMove(_closedPos, closeDuration)
            .SetEase(closeEase)
            .OnComplete(() => _isAnimating = false);
    }

    void ScheduleClose()
    {
        if (_closeTween != null && _closeTween.IsActive()) return;
        _closeTween = DOVirtual.DelayedCall(closeDelay, Close);
    }

    public void Toggle() { if (_isOpen) Close(); else Open(); }

    public bool IsOpen => _isOpen;
    public bool IsAnimating => _isAnimating;

    void OnDestroy()
    {
        _closeTween?.Kill();
        door?.DOKill();
    }

    // ========== GIZMOS ==========
#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, openRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, closeRadius);

        if (door == null) return;

        Vector3 closed = Application.isPlaying ? _closedPos : door.localPosition;
        Vector3 open = closed + slideAxis.normalized * slideDistance;

        Transform p = door.parent != null ? door.parent : door;
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(p.TransformPoint(closed), p.TransformPoint(open));
        Gizmos.DrawWireCube(p.TransformPoint(open), Vector3.one * 0.15f);
    }
#endif
}