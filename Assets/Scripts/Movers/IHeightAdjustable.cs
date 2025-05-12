public interface IHeightAdjustable
{
    void MoveToHeight(float targetY, float speed);
    void Tick(float deltaTime);
}
