using UnityEngine;

public class FlamethrowerTrap : MonoBehaviour
{
    public ParticleSystem fireParticle; // Particle System cho lửa
    public float fireOnDuration = 2f;   // Thời gian phun lửa
    public float fireOffDuration = 2f;  // Thời gian tắt lửa

    private float timer;
    private bool isFiring = false;

    void Start()
    {
        if (fireParticle != null)
        {
            fireParticle.Stop(); // Bắt đầu với việc lửa tắt
        }
        timer = fireOffDuration; // Để lửa tắt ban đầu
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (isFiring && timer <= 0)
        {
            // Tắt lửa
            fireParticle.Stop();
            isFiring = false;
            timer = fireOffDuration; // Đặt lại thời gian tắt lửa
        }
        else if (!isFiring && timer <= 0)
        {
            // Bật lửa
            fireParticle.Play();
            isFiring = true;
            timer = fireOnDuration; // Đặt lại thời gian bật lửa
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player đã vào vùng Flamethrower Trap!");
            // Xử lý logic khi Player vào vùng lửa, ví dụ gây sát thương.
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && isFiring)
        {
            Debug.Log("Player đang ở trong vùng lửa và bị sát thương!");
            // Gây sát thương cho Player ở đây nếu cần
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player đã rời khỏi vùng Flamethrower Trap!");
        }
    }
}
