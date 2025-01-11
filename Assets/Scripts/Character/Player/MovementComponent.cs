using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class MovementComponent : MonoBehaviour, IMovement, IJump
{
    [Header("Moving")]
    
    [SerializeField] private float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }
    public float CurrentSpeed { get; private set; }
    private int directionMoving;
    public int DirectionMoving { 
        get {
            return directionMoving;
        }
        set {
            directionMoving = (value > 0)?1:(value < 0)?-1:0;
        }
    }
    public bool IsMoving { get; private set; }
    public LayerMask GroundMask;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    public float JumpForce { get => jumpForce; private set => jumpForce = value; }
    public bool IsGrounded { get; private set; }
    [SerializeField] private Vector3 groundCheckOffset;
    public Vector3 GroundCheckOffset { get => groundCheckOffset; private set => groundCheckOffset = value; }




    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        GroundCheck();
        Move(DirectionMoving);
    }
    /// <summary>
    /// Передвигает объект в зависимости от направления
    /// </summary>
    /// <param name="direction">-1 - в лево, 1 - в право, 0 - остановиться</param>
    public void Move(int direction)
    {

        CurrentSpeed = MoveSpeed * direction;


        transform.position += new Vector3(CurrentSpeed * Time.deltaTime, 0, 0);
        IsMoving = (Mathf.Abs(CurrentSpeed) > 0) ? true : false;
        CharacterAnimatorController.Instance.SetMovementState(IsMoving);

        if (IsMoving && spriteRenderer != null) spriteRenderer.flipX = (direction > 0) ? false : true;
    }

    public void Jump()
    {
        if (IsGrounded)
        {
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            CharacterAnimatorController.Instance.TriggerJump();
        }
    }

    public bool IsFlying()
    {
        if (rb.linearVelocity.y < 0) return true;
        else return false;
    }
    public void GroundCheck()
    {
        
        float rayLength = 0.3f;
        Vector3 rayStartPosition = transform.position + GroundCheckOffset;
        RaycastHit2D hit = Physics2D.Raycast(rayStartPosition, Vector3.down, rayLength, GroundMask);

        IsGrounded = hit.collider != null;

        Debug.DrawLine(rayStartPosition, rayStartPosition + Vector3.down * rayLength, Color.red);
    }
}