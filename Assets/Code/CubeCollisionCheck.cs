using System.Collections;
using UnityEngine;

public class CubeCollisionCheck : MonoBehaviour
{
    // Tùy chọn để chỉ định phạm vi kiểm tra va chạm.
    public float checkRadius = 5f;
    public LayerMask collisionLayer; // Để xác định các đối tượng mà Cube sẽ va chạm với
    public float fadeDuration = 2f;  // Thời gian mờ dần (giây)

    private void Update()
    {
        // Kiểm tra va chạm trong phạm vi bán kính nhất định
        CheckCollision();
    }

    private void CheckCollision()
    {
        // Dùng OverlapSphere để kiểm tra các đối tượng trong phạm vi xung quanh Cube
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius, collisionLayer);

        // Nếu có va chạm, thực hiện hành động
        if (hitColliders.Length > 0)
        {
            foreach (var hitCollider in hitColliders)
            {
                // Kiểm tra nếu va chạm với đối tượng có tag "Player"
                if (hitCollider.CompareTag("Player"))
                {
                    Debug.Log("Cube đã va chạm với Player");

                    // Bắt đầu hiệu ứng mờ dần và xóa Cube
                    StartCoroutine(FadeAndDestroy(gameObject));
                }
            }
        }
    }

    // Coroutine để làm mờ đối tượng dần và xóa nó sau khi mờ hết
    private IEnumerator FadeAndDestroy(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer == null) yield break; // Nếu đối tượng không có Renderer thì không làm gì

        Material material = renderer.material;
        Color originalColor = material.color;
        float elapsedTime = 0f;

        // Mờ dần (giảm alpha) trong thời gian fadeDuration
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Đảm bảo đối tượng hoàn toàn trong suốt trước khi xóa
        material.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        // Xóa đối tượng sau khi fade out hoàn tất
        Destroy(obj);
    }

    private void OnDrawGizmos()
    {
        // Hiển thị phạm vi bán kính để debug
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
