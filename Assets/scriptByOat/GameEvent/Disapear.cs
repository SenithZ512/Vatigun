using UnityEngine;

public class Disapear : MonoBehaviour
{
    [SerializeField] private float delay = 3f;

    private void OnEnable()
    {
        // เมื่อ Object ถูกดึงออกจาก Pool (SetActive(true)) ให้เริ่มนับถอยหลัง
        Invoke(nameof(Deactivate), delay);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        // ยกเลิก Invoke ถ้า Object ถูกปิดไปก่อนเวลา (เพื่อความปลอดภัย)
        CancelInvoke();
    }
}
