using StateEnumerators;

public interface IProjectile
{
    void SetDirection(Directions direction);
    Directions GetDirection();
    float GetKnockback();
    void SetSafeTags();
}
