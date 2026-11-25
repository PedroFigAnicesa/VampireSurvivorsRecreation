
using UnityEngine;

public class PlayerMouseShooting : MonoBehaviour
{
    [Header("Configurações do projétil")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 15f;

    [Header("Configurações de tiro")]
    public float fireCooldown = 0.5f;
    public float spawnDistance = 0.8f;

    private float nextFireTime = 0f;

    void Update()
    {
        // Verifica se o cooldown terminou
        if (Time.time >= nextFireTime)
        {
            Transform nearestEnemy = FindNearestEnemy();

            // Só atira se existir algum inimigo
            if (nearestEnemy != null)
            {
                Shoot(nearestEnemy);
                nextFireTime = Time.time + fireCooldown;
            }
        }
    }

    Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Transform nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(
                transform.position,
                enemy.transform.position
            );

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }

        return nearestEnemy;
    }

    void Shoot(Transform target)
    {
        // Calcula a direção até o inimigo
        Vector2 shootDirection =
            ((Vector2)target.position - (Vector2)transform.position).normalized;

        // Define onde a bala será criada
        Vector3 spawnPosition =
            transform.position + (Vector3)(shootDirection * spawnDistance);

        // Instancia o projétil
        GameObject bullet = Instantiate(
            bulletPrefab,
            spawnPosition,
            Quaternion.identity
        );

        // Rotaciona o projétil na direção do inimigo
        float angle = Mathf.Atan2(
            shootDirection.y,
            shootDirection.x
        ) * Mathf.Rad2Deg;

        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Aplica velocidade
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = shootDirection * bulletSpeed;
        }
    }
}
