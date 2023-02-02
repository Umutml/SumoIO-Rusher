using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    public static CollectableSpawner Instance;

    [SerializeField] private GameObject collectablePrefab;
    [SerializeField] private Transform collectableParent;
    public List<GameObject> foodList = new();

    private void Awake()
    {
        Instance = this;
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


    private void SpawnCollectable()
    {
        var spawnPosition = new Vector3(Random.Range(-15, 16), 1.5f, Random.Range(-5.25f, 21.5f));
        var cloneSpawn = Instantiate(collectablePrefab, spawnPosition, Quaternion.identity, transform);
        cloneSpawn.transform.localScale = Vector3.zero;
        cloneSpawn.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.25f);
        foodList.Add(cloneSpawn);
    }
}