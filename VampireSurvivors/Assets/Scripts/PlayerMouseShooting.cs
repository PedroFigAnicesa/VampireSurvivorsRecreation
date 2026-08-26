using UnityEngine;

public class PlayerMouseShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 15f;

    [Header("Configurações de Tiro")]
    public float fireCooldown = 0.2f; // Tempo de espera entre os tiros
    private float nextFireTime = 0f;

    void Update()
    {
        // Botão esquerdo do mouse pressionado E o cooldown liberado
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireCooldown;
        }
    }

    void Shoot()
    {
        // 1. Pega a posição do mouse na tela e converte para o mundo do jogo (2D)
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0f; // Zera o Z para garantir que fique no plano 2D

        // 2. Calcula a direção do tiro (do player até o mouse) e normaliza (deixa com tamanho 1)
        Vector2 shootDirection = (mouseWorldPosition - transform.position).normalized;

        // 3. Define a posição onde a bala nasce (um pouco afastada do centro do player)
        Vector3 spawnPosition = transform.position + (Vector3)(shootDirection * 0.8f);

        // 4. Cria a bala
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        // 5. Opcional: Faz a bala rotacionar para apontar na direção do tiro
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // 6. Aplica a velocidade no Rigidbody2D da bala
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = shootDirection * bulletSpeed;
        }
    }
}