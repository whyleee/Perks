namespace Perks.Data
{
    public interface IStorage
    {
        T Get<T>(object key);

        void Set(object key, object value);

        void Remove(object key);
    }
}