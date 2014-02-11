namespace Perks.Data
{
    public interface IFsItem
    {
        string Path { get; }

        string Name { get; set; }
    }
}