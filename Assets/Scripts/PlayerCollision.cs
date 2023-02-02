using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private float scaleMultiplier;
    [SerializeField] private int playerLevel;
    [SerializeField] private float pushForce = 5;
    private PlayerMovement playerMoveScript;
    private Rigidbody playerRb;
    private static readonly int IsRunning = Animator.StringToHash("isRunning");

    private void Start()
    {
        playerLevel = 1;
        playerMoveScript = GetComponent<PlayerMovement>();
        playerRb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyCollision(collision);
        GroundCheck(collision);
    }
    private void OnCollisionExit(Collision other)
    {
        GroundExitColl(other);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        FoodCollision(other);
        EnemyTrigger(other);
        WaterCollision(other);
    }

    private void GroundCheck(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && GameManager.Instance.gameStarted)
        {
            playerMoveScript.canMove = true;
            playerMoveScript.playerAnimator.SetBool(IsRunning, true);
        }
    }

    private void EnemyCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerMoveScript.canMove = false;
            collision.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            StartCoroutine(EnemyNavmeshActivate(collision.gameObject));
            
            playerRb.AddForce(-transform.forward * pushForce / 4, ForceMode.Impulse); // OurRigidbody Force
            Invoke(nameof(CanMoveAfterForce), pushForce / 130);                  // Wait for pushComplete after player can move
            
            var otherRb =collision.gameObject.GetComponent<Rigidbody>();            // This part could also be written using DoTween, which would likely have better control and performance.
            otherRb.AddForce(transform.forward * pushForce, ForceMode.Impulse);    //  I used physics for prototyping purposes.
        }
    }

    IEnumerator EnemyNavmeshActivate(GameObject enemy)
    {
        yield return new WaitForSeconds(.2f);
        enemy.GetComponent<NavMeshAgent>().enabled = true;

    }

    private static void WaterCollision(Collider other)
    {
        if (other.CompareTag("Water"))
            EventManager.OnGameOver();
    }

    private void EnemyTrigger(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            playerMoveScript.canMove = false;                                                     //
            other.GetComponent<NavMeshAgent>().enabled = false;
            StartCoroutine(EnemyNavmeshActivate(other.gameObject));
            
            playerRb.AddForce(-transform.forward * pushForce / 4, ForceMode.Impulse);            //
            Invoke(nameof(CanMoveAfterForce), pushForce / 130);                             // Weakpoint Collision

            var otherRb = other.GetComponent<Rigidbody>();                                     //
            otherRb.AddForce(transform.forward * (pushForce * 1.75f), ForceMode.Impulse);     //    
        }
    }
    private void FoodCollision(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            playerLevel++;
            Destroy(other.gameObject);
            StatsUp();
            EventManager.OnCollectableTaken();          // If we take 1 object call event, SpawnManager spawn collectable once random position
            CollectableSpawner.Instance.foodList.Remove(other.gameObject);
        }
    }

    private void GroundExitColl(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            playerMoveScript.canMove = false;
            playerMoveScript.playerAnimator.SetBool(IsRunning, false);
        }
    }

    private void CanMoveAfterForce()
    {
        playerMoveScript.canMove = true;
    }

    private void StatsUp()
    {
        transform.DOScale(CalculateScale(playerLevel), 1f);
        playerMoveScript.moveSpeed -= 0.15f;
        pushForce += 5;
    }

    private Vector3 CalculateScale(int level)
    {
        var playerScale = 1.5f + level * scaleMultiplier;
        playerScale = Mathf.Clamp(playerScale, 1f, 7f);
        var scale = Vector3.one * playerScale;
        return scale;
    }
}