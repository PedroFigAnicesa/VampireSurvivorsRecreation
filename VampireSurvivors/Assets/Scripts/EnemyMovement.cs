
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Configurações")]
    public float moveSpeed = 3f;

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Procura o jogador pela Tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void FixedUpdate()
    {
        if (player == null || rb == null)
            return;

        // Calcula a direção até o jogador
        Vector2 direction = ((Vector2)player.position - rb.position).normalized;

        // Movimenta o inimigo
        rb.linearVelocity = direction * moveSpeed;
    }
}
