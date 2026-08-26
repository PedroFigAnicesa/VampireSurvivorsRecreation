using UnityEngine;

public class PlayerShooting2D : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 12f;

    [Header("Configurações de Tiro")]
    public float fireCooldown = 0.3f; // Tempo de espera entre os tiros (em segundos)
    private float nextFireTime = 0f;  // Controla quando o player poderá atirar novamente

    void Update()
    {
        float shootX = 0f;
        float shootY = 0f;

        if (Input.GetKeyDown(KeyCode.UpArrow))    shootY = 1f;
        else if (Input.GetKeyDown(KeyCode.DownArrow))  shootY = -1f;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))  shootX = -1f;
        else if (Input.GetKeyDown(KeyCode.RightArrow)) shootX = 1f;

        // Se apertou alguma seta E o tempo atual já passou do limite do cooldown
        if ((shootX != 0f || shootY != 0f) && Time.time >= nextFireTime)
        {
            Shoot(new Vector2(shootX, shootY));
            
            // Define o próximo momento em que o player poderá atirar
            nextFireTime = Time.time + fireCooldown;
        }
    }

    void Shoot(Vector2 direction)
    {
        Vector3 spawnPosition = transform.position + (Vector3)(direction * 0.8f);
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }
    }
}