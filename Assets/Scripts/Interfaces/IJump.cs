using UnityEngine;

public interface IJump
{
    Vector3 GroundCheckOffset { get; }
    float JumpForce { get; }
    bool IsGrounded { get; }
    void Jump();
    bool IsFlying();
    void GroundCheck();
}
