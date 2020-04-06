namespace EMT
{
    public interface IResource
    {
        bool IsEmpty();
        bool IsFull();
        bool HasAtLeast(float leastValue);
        void Fill();
        void Deplete();
        void Add(float amount);
    }
}