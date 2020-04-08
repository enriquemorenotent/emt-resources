namespace EMT
{
    public interface IResourceModifier
    {
        bool HasExpired();

        // Returns the amount to modify
        float Execute(Resource resource, float deltaTime);
    }
}