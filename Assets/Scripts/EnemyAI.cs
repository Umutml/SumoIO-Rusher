using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    [SerializeField] private float scaleMultiplier;
    private Animator enemyAnimator;
    private NavMeshAgent enemyNavmesh;
    private int myLevel;
    private bool targetLocated;
    private Vector3 targetTransform;

    private void Start()
    {
        myLevel = 1;
        enemyAnimator = GetComponent<Animator>();
        enemyNavmesh = GetComponent<NavMeshAgent>();
        ChooseTarget();
        StartCoroutine(TargetLocatedFalse());
    }

    private void Update()
    {
        if (!targetLocated && enemyAnimator.enabled) ChooseTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            myLevel++;
            Destroy(other.gameObject);
            StatsUp();
            EventManager
                .OnCollectableTaken(); // If we take 1 object call event, SpawnManager spawn collectable once random position
            CollectableSpawner.Instance.foodList.Remove(other.gameObject);
            targetLocated = false;
        }

        if (other.CompareTag("Water"))
        {
            enemyNavmesh.enabled = false;
            GameObject o;
            (o = gameObject).SetActive(false);
            Destroy(o);
        }
    }

    private IEnumerator TargetLocatedFalse()
    {
        while (true)
        {
            yield return new WaitForSeconds(8);
            if (enemyNavmesh.enabled) targetLocated = false;
        }
    }

    private void ChooseTarget()
    {
        /*Collider[] hitColliders = Physics.OverlapSphere(transform.position, physicRadius);
        var MyCollectables = new List<Vector3>();
        for (var i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].CompareTag("Food"))
            {
                MyCollectables.Add(hitColliders[i].transform.position);
            }
        }
        if (MyCollectables.Count > 0)
        {
            targetTransform = MyCollectables[0];                                
        }*/


        //var foods = CollectableSpawner.Instance.foodList;
        //var randomTarget = CollectableSpawner.Instance.foodList[Random.Range(0,foods.Count)];
        //targetTransform = randomTarget.transform.position;

        var randomListTarget =
            CollectableSpawner.Instance.foodList[Random.Range(0, CollectableSpawner.Instance.foodList.Count)];
        //Debug.Log(randomListTarget.name);
        if (!enemyNavmesh.enabled) return;

        enemyNavmesh.SetDestination(randomListTarget.transform.position);
        targetLocated = true;
        enemyAnimator.SetBool(IsRunning, true);
    }

    private void StatsUp()
    {
        transform.DOScale(CalculateScale(myLevel), 1f);
        enemyNavmesh.speed -= 0.05f;
    }

    private Vector3 CalculateScale(int level)
    {
        var playerScale = 1.5f + level * scaleMultiplier;
        playerScale = Mathf.Clamp(playerScale, 1f, 7f);
        var scale = Vector3.one * playerScale;
        return scale;
    }
}