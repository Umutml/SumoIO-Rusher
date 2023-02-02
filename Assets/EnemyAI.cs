using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    private bool targetLocated;
    private Animator enemyAnimator;
    private NavMeshAgent enemyNavmesh;
    private int myLevel;
    [SerializeField] private float scaleMultiplier;
    [SerializeField] private float pushForce = 5;
    private Rigidbody myRb;
    
    [SerializeField]private float physicRadius;
    
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private Vector3 targetTransform;
    
    


    // Start is called before the first frame update
    void Start()
    {
        myLevel = 1;
        enemyAnimator = GetComponent<Animator>();
        enemyNavmesh = GetComponent<NavMeshAgent>();
        myRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //if (!targetLocated) ChooseTarget();
        //ChooseTarget();
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

        //if (targetTransform == null) return;
       
        var foods = CollectableSpawner.Instance.foodList;
        var randomTarget = CollectableSpawner.Instance.foodList.Random();
        targetTransform = randomTarget.transform.position;

        if (targetTransform == null)
        {
            enemyNavmesh.SetDestination(targetTransform);
        }
        
        enemyAnimator.SetBool(IsRunning, true);

    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, physicRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            myLevel++;
            Debug.Log("Food Taken");
            Destroy(other.gameObject);
            //StatsUp();
            EventManager.OnCollectableTaken();          // If we take 1 object call event, SpawnManager spawn collectable once random position
            CollectableSpawner.Instance.foodList.Remove(other.gameObject);
            //targetLocated = false;
        }
        
        if (other.CompareTag("Water"))
        {
            enemyNavmesh.enabled = false;
            this.gameObject.SetActive(false);
        }
    }
    
    private void StatsUp()
    {
        transform.DOScale(CalculateScale(myLevel), 1f);
        enemyNavmesh.speed -= 0.15f;
        pushForce += 5;
        Debug.Log("Scale UP");
    }
    
    private Vector3 CalculateScale(int level)
    {
        var playerScale = 1.5f + level * scaleMultiplier;
        playerScale = Mathf.Clamp(playerScale, 1f, 7f);
        var scale = Vector3.one * playerScale;
        return scale;
    }
}
