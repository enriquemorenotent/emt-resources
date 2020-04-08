namespace EMT
{
    public interface IResourceModifier
    {
        bool HasExpired();

        // Returns the amount to modify
        float Execute(float deltaTime);
    }
}