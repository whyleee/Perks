namespace Perks.Data
{
    public interface IFileStorage : IOldRepository<IFsItem>
    {
        //T Create<T>(string path) where T : IFsItem;

        T Get<T>(string path) where T : IFsItem;

        bool Exists(string path);

        void Copy(IFsItem item, string to);

        void Copy(string from, string to);

        void Move(IFsItem item, string to);

        void Move(string from, string to);

        void Replace(IFsItem item, string by, string backupTo);

        void Replace(string target, string by, string backupTo);

        void Delete(string path);
    }
}