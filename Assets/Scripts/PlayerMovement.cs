using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    
    public float moveSpeed = 5;
    private float turnSpeed = 5;
    public bool canMove;
    
    [SerializeField] private FloatingJoystick floatingJoystick;
    
    private Vector3 moveDirection;
    private Animator playerAnimator;
    private Rigidbody playerRigidbody;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
        TurnCharacter();
    }
    private void OnEnable()
    {
        EventManager.OnGameStartedEvent += PlayRunAnim;
    }

    private void OnDisable()
    {
        EventManager.OnGameStartedEvent -= PlayRunAnim;
    }

    private void TurnCharacter()
    {
        if (floatingJoystick.Horizontal != 0)
        {
            moveDirection = new Vector3(floatingJoystick.Horizontal, 0, floatingJoystick.Vertical); // TODO: Lerp turning speed
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * turnSpeed);
        }
    }

    private void Move()
    {
        if (canMove) playerRigidbody.velocity = transform.forward * moveSpeed;
    }

    private void PlayRunAnim()
    {
        playerAnimator.SetBool(IsRunning, true);
        canMove = true;
    }
}