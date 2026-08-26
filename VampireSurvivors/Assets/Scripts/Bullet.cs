using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 3f; // Tempo em segundos para a bala sumir

    void Start()
    {
        // Destroi a bala automaticamente após o tempo limite
        Destroy(gameObject, lifetime);
    }

    // Opcional: Destruir ao bater em algo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se bater em algo que não seja o player, se destrói
        if (!collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}