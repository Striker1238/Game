public interface IMovement
{
    float MoveSpeed { get; }
    float CurrentSpeed { get; }
    bool IsMoving { get; }
    void Move(int direction);
}