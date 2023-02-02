using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{

    [SerializeField] private GameObject collectablePrefab;

    private void Start()
    {
        //SpawnCollectables(50);
    }

    private void OnEnable()
    {
        EventManager.OnCollectableTakenEvent += SpawnCollectable;
    }

    private void OnDisable()
    {
        EventManager.OnCollectableTakenEvent -= SpawnCollectable;
    }

    private void SpawnCollectables(int count)
    {
        for (var i = 0; i < count; i++)
        {
            var spawnPosition = new Vector3(
                Random.Range(-15, 16), 1.5f, Random.Range(-5.25f, 21.5f)
            );
            Instantiate(collectablePrefab, spawnPosition, Quaternion.identity, transform);
        }
    }

    public void SpawnCollectable()
    {
        var spawnPosition = new Vector3(Random.Range(-15, 16), 1.5f, Random.Range(-5.25f, 21.5f));
        Instantiate(collectablePrefab, spawnPosition, Quaternion.identity, transform);
    }
}