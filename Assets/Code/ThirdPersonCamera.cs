using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;  // Link đến đối tượng người chơi
    public float distance = 5.0f;  // Khoảng cách camera đến người chơi
    public float height = 2.0f;  // Chiều cao camera so với người chơi
    public float rotationSpeed = 5.0f;  // Tốc độ quay camera
    public float verticalRotationLimit = 80f;  // Giới hạn góc quay theo chiều dọc

    private float currentRotationX = 0.0f;  // Góc quay theo chiều dọc
    private float currentRotationY = 0.0f;  // Góc quay theo chiều ngang

    void Start()
    {
        // Ẩn con trỏ chuột và giữ nó cố định vào giữa màn hình khi bắt đầu game
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Lấy input chuột để xoay camera
        float horizontalInput = Input.GetAxis("Mouse X");
        float verticalInput = Input.GetAxis("Mouse Y");

        // Cập nhật góc quay theo chiều ngang (quay quanh trục Y)
        currentRotationY += horizontalInput * rotationSpeed;

        // Cập nhật góc quay theo chiều dọc (quay quanh trục X) và đảm bảo không vượt quá giới hạn
        currentRotationX -= verticalInput * rotationSpeed;
        currentRotationX = Mathf.Clamp(currentRotationX, -verticalRotationLimit, verticalRotationLimit);

        // Tính toán vị trí mới của camera dựa trên góc quay
        Vector3 direction = new Vector3(0, height, -distance);  // Vị trí camera tính từ người chơi
        Quaternion rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0);  // Quay camera quanh trục X và Y
        Vector3 cameraPosition = player.position + rotation * direction;  // Vị trí camera

        // Cập nhật vị trí và hướng nhìn của camera
        transform.position = cameraPosition;
        transform.LookAt(player);  // Đảm bảo camera luôn nhìn vào nhân vật
    }
}
