using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [Header("��������� ������")]
    [SerializeField] private GameObject prefabToSpawn;   // ������ ��� ������
    [SerializeField] private GameObject spawnAreaObject;  // ������, �������� ������� 
    [SerializeField] private float checkRadius = 1f;    // ������ �������� ��������
    [SerializeField] private int enemySpawnCount = 3; //����������� ����������� ������
    private int maxAttempts = 50;        // ������������ ���������� ������� ������ �������

    [Inject]  private DiContainer diContainer;


    private void Start()
    {
        for (int i = 0; i < enemySpawnCount; i++)
        {
            SpawnEnemy();
        }
    }

    //����� ������ ����� zenject
    public void SpawnEnemy()
    {
        Vector3 spawnPos = GetRandomSpawnPosition();
        if (spawnPos != Vector3.zero)
        {      
            diContainer.InstantiatePrefab(prefabToSpawn, spawnPos, Quaternion.identity, null);
        }
    }

    //���������� ��������� ������� ������ ������� spawnAreaObject ��� ��������
    public Vector3 GetRandomSpawnPosition()
    {
        if (spawnAreaObject == null)
        {
            Debug.LogError("�� ����� ������ ������� ������!");
            return Vector3.zero;
        }

        // �������� ������� ������� ������
        Bounds spawnBounds = GetSpawnAreaBounds();

        for (int i = 0; i < maxAttempts; i++)
        {
            // ���������� ��������� ����� ������ bounds
            Vector3 randomPos = new Vector3(
                Random.Range(spawnBounds.min.x, spawnBounds.max.x),
                Random.Range(spawnBounds.min.y, spawnBounds.max.y),
                Random.Range(spawnBounds.min.z, spawnBounds.max.z)
            );

            // ��������� �������� 
            bool isPositionFree = CheckIfPositionFree(randomPos);
            if (isPositionFree)
            {
                return randomPos;
            }
        }

        return Vector3.zero;
    }

    // ���������, �������� �� ������� �� ��������
    private bool CheckIfPositionFree(Vector3 position)
    {
        // ��� 2D
        if (Physics2D.OverlapCircle(position, checkRadius) == null)
        {
            return true;
        }

        return false;
    }

    // �������� ������� ������� ������ �� �������
    private Bounds GetSpawnAreaBounds()
    {
        // ���� � ������� ���� Collider2D (��� 2D)
        Collider2D collider2D = spawnAreaObject.GetComponent<Collider2D>();
        if (collider2D != null)
            return collider2D.bounds;

        // ���� � ������� ���� Collider (��� 3D)
        Collider collider3D = spawnAreaObject.GetComponent<Collider>();
        if (collider3D != null)
            return collider3D.bounds;

        // ���� � ������� ���� SpriteRenderer (2D ������)
        SpriteRenderer spriteRenderer = spawnAreaObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            return spriteRenderer.bounds;

        // ���� � ������� ���� MeshRenderer (3D ������, ��������, Plane)
        MeshRenderer meshRenderer = spawnAreaObject.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
            return meshRenderer.bounds;

        // ���� ������ �� �������, ���������� ��������� �������
        Debug.LogWarning("�� ������ Collider/SpriteRenderer/MeshRenderer. ������������ ��������� �������.");
        return new Bounds(spawnAreaObject.transform.position, spawnAreaObject.transform.localScale);
    }
}