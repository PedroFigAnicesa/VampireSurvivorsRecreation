using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float moveSpeed = 3f;
    private Transform playerTransform;

    void Start()
    {
        // Encontra o player automaticamente pela tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        // Move em direção ao player a cada frame (perseguição infinita)
        transform.position = Vector3.MoveTowards(
            transform.position, 
            playerTransform.position, 
            moveSpeed * Time.deltaTime
        );

        
        Vector3 direction = playerTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}