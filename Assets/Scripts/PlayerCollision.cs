using System;
using DG.Tweening;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private float scaleMultiplier;
    [SerializeField] private int playerLevel;
    [SerializeField] private float pushForce = 5;
    private PlayerMovement playerMoveScript;
    private Rigidbody playerRb;

    private void Start()
    {
        playerLevel = 1;
        playerMoveScript = GetComponent<PlayerMovement>();
        playerRb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerMoveScript.canMove = false;
            playerRb.AddForce(-transform.forward * pushForce / 2, ForceMode.Impulse); // OurRigidbody Force
            Invoke(nameof(CanMoveAfterForce), pushForce / 100);                   // Wait for pushComplete after player can move
            
            var otherRb = collision.gameObject.GetComponent<Rigidbody>();             // This part could also be written using DoTween, which would likely have better control and performance.
            otherRb.AddForce(transform.forward * pushForce, ForceMode.Impulse);       //  I used physics for prototyping purposes.
        }
        if (collision.gameObject.CompareTag("Ground") && GameManager.Instance.gameStarted)
        {
            playerMoveScript.canMove = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            playerLevel++;
            Debug.Log("Food Taken");
            Destroy(other.gameObject);
            StatsUp();
            EventManager.OnCollectableTaken(); // If we take 1 object call event, SpawnManager spawn collectable once random position
        }
        if (other.CompareTag("Enemy"))
        {
            playerMoveScript.canMove = false;                                               //
            playerRb.AddForce(-transform.forward * pushForce / 2, ForceMode.Impulse);       //
            Invoke(nameof(CanMoveAfterForce), pushForce / 100);                         // Weakpoint Collision

            var otherRb = other.GetComponent<Rigidbody>();                                  //
            otherRb.AddForce(transform.forward * (pushForce * 1.5f), ForceMode.Impulse);    //    
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            playerMoveScript.canMove = false;
        }
    }

    private void CanMoveAfterForce()
    {
        playerMoveScript.canMove = true;
    }

    private void StatsUp()
    {
        transform.DOScale(CalculateScale(playerLevel), 1f);
        playerMoveScript.moveSpeed -= 0.10f;
        pushForce += 5;
        Debug.Log("Scale UP");
    }

    private Vector3 CalculateScale(int level)
    {
        var playerScale = 2 + level * scaleMultiplier;
        playerScale = Mathf.Clamp(playerScale, 2f, 5f);
        var scale = Vector3.one * playerScale;
        return scale;
    }
}