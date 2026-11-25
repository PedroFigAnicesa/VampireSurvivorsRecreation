
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Referências")]
    public GameObject enemyPrefab;
    public Transform player;
    public Camera mainCamera;

    [Header("Configurações de geração")]
    public float spawnInterval = 2f;
    public float spawnDistance = 2f;

    private float nextSpawnTime;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null || player == null || mainCamera == null)
            return;

        // Limites da câmera no mundo 2D
        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        Vector2 cameraCenter = mainCamera.transform.position;

        float halfWidth = cameraWidth;
        float halfHeight = cameraHeight;

        // Sorteia uma das quatro bordas
        int side = Random.Range(0, 4);

        Vector2 spawnPosition;

        switch (side)
        {
            case 0: // Superior
                spawnPosition = new Vector2(
                    Random.Range(-halfWidth, halfWidth),
                    halfHeight + spawnDistance
                );
                break;

            case 1: // Inferior
                spawnPosition = new Vector2(
                    Random.Range(-halfWidth, halfWidth),
                    -halfHeight - spawnDistance
                );
                break;

            case 2: // Direita
                spawnPosition = new Vector2(
                    halfWidth + spawnDistance,
                    Random.Range(-halfHeight, halfHeight)
                );
                break;

            default: // Esquerda
                spawnPosition = new Vector2(
                    -halfWidth - spawnDistance,
                    Random.Range(-halfHeight, halfHeight)
                );
                break;
        }

        // Converte a posição relativa à câmera para coordenadas do mundo
        spawnPosition += cameraCenter;

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
