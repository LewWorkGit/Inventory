using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [Header("Настройки спавна")]
    [SerializeField] private GameObject prefabToSpawn;   // Префаб для спавна
    [SerializeField] private GameObject spawnAreaObject;  // Объект, задающий область 
    [SerializeField] private float checkRadius = 1f;    // Радиус проверки коллизий
    [SerializeField] private int enemySpawnCount = 3; //Колличество спавнящихся врагов
    private int maxAttempts = 50;        // Максимальное количество попыток поиска позиции

    [Inject]  private DiContainer diContainer;


    private void Start()
    {
        for (int i = 0; i < enemySpawnCount; i++)
        {
            SpawnEnemy();
        }
    }

    //спавн врагов через zenject
    public void SpawnEnemy()
    {
        Vector3 spawnPos = GetRandomSpawnPosition();
        if (spawnPos != Vector3.zero)
        {      
            diContainer.InstantiatePrefab(prefabToSpawn, spawnPos, Quaternion.identity, null);
        }
    }

    //Возвращает случайную позицию внутри области spawnAreaObject без коллизий
    public Vector3 GetRandomSpawnPosition()
    {
        if (spawnAreaObject == null)
        {
            Debug.LogError("Не задан объект области спавна!");
            return Vector3.zero;
        }

        // Получаем границы области спавна
        Bounds spawnBounds = GetSpawnAreaBounds();

        for (int i = 0; i < maxAttempts; i++)
        {
            // Генерируем случайную точку внутри bounds
            Vector3 randomPos = new Vector3(
                Random.Range(spawnBounds.min.x, spawnBounds.max.x),
                Random.Range(spawnBounds.min.y, spawnBounds.max.y),
                Random.Range(spawnBounds.min.z, spawnBounds.max.z)
            );

            // Проверяем коллизии 
            bool isPositionFree = CheckIfPositionFree(randomPos);
            if (isPositionFree)
            {
                return randomPos;
            }
        }

        return Vector3.zero;
    }

    // Проверяет, свободна ли позиция от коллизий
    private bool CheckIfPositionFree(Vector3 position)
    {
        // Для 2D
        if (Physics2D.OverlapCircle(position, checkRadius) == null)
        {
            return true;
        }

        return false;
    }

    // Получает границы области спавна из объекта
    private Bounds GetSpawnAreaBounds()
    {
        // Если у объекта есть Collider2D (для 2D)
        Collider2D collider2D = spawnAreaObject.GetComponent<Collider2D>();
        if (collider2D != null)
            return collider2D.bounds;

        // Если у объекта есть Collider (для 3D)
        Collider collider3D = spawnAreaObject.GetComponent<Collider>();
        if (collider3D != null)
            return collider3D.bounds;

        // Если у объекта есть SpriteRenderer (2D спрайт)
        SpriteRenderer spriteRenderer = spawnAreaObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            return spriteRenderer.bounds;

        // Если у объекта есть MeshRenderer (3D объект, например, Plane)
        MeshRenderer meshRenderer = spawnAreaObject.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
            return meshRenderer.bounds;

        // Если ничего не найдено, используем дефолтные границы
        Debug.LogWarning("Не найден Collider/SpriteRenderer/MeshRenderer. Используются локальные границы.");
        return new Bounds(spawnAreaObject.transform.position, spawnAreaObject.transform.localScale);
    }
}