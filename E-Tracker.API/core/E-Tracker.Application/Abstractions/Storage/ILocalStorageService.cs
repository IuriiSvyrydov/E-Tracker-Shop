namespace E_Tracker.Application.Abstractions.Storage
{
    public interface ILocalStorageService : IStorage
    {
        public string LocalStorageName { get; set; }
    }
}
