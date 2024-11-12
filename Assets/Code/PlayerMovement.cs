using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;  // Tốc độ di chuyển
    public float jumpHeight = 2.0f;  // Chiều cao nhảy
    public float gravity = -9.81f;  // Lực hấp dẫn

    private CharacterController controller;
    private Vector3 velocity;

    public ThirdPersonCamera cameraScript;  // Tham chiếu đến script camera để lấy hướng quay

    void Start()
    {
        // Lấy component CharacterController
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Lấy input từ người chơi với các phím W, A, S, D
        float moveX = 0f;
        float moveZ = 0f;

        // Di chuyển theo phím W, A, S, D
        if (Input.GetKey("w")) moveZ = 1f;  // W - Tiến lên
        if (Input.GetKey("s")) moveZ = -1f; // S - Lùi lại
        if (Input.GetKey("a")) moveX = -1f; // A - Quay trái
        if (Input.GetKey("d")) moveX = 1f;  // D - Quay phải

        // Lấy hướng di chuyển từ camera
        Vector3 forward = cameraScript.transform.forward;
        Vector3 right = cameraScript.transform.right;

        // Đảm bảo rằng hướng di chuyển không bị thay đổi theo trục Y (vì camera có thể quay lên xuống)
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // Tính toán vector di chuyển dựa trên hướng camera
        Vector3 move = (forward * moveZ + right * moveX) * moveSpeed;

        // Di chuyển nhân vật
        controller.Move(move * Time.deltaTime);

        // Nhảy khi người chơi nhấn phím nhảy
        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);  // Tính toán lực nhảy
        }

        // Áp dụng lực hấp dẫn
        velocity.y += gravity * Time.deltaTime;

        // Di chuyển nhân vật với lực hấp dẫn
        controller.Move(velocity * Time.deltaTime);
    }
}
