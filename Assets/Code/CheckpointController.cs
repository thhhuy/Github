using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public Transform startCheckpoint; // Điểm checkpoint bắt đầu
    public Transform endCheckpoint; // Điểm checkpoint kết thúc

    private void Start()
    {
        if (startCheckpoint == null || endCheckpoint == null)
        {
            Debug.LogWarning("Checkpoint chưa được gán trong Inspector!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Kiểm tra nếu Player chạm vào điểm bắt đầu
            if (other.transform.position == startCheckpoint.position)
            {
                Debug.Log("Player đã chạm vào Checkpoint Bắt Đầu!");
            }
            // Kiểm tra nếu Player chạm vào điểm kết thúc
            else if (other.transform.position == endCheckpoint.position)
            {
                Debug.Log("Player đã chạm vào Checkpoint Kết Thúc!");
            }
        }
    }
}
