namespace E_Tracker.Application.Abstractions
{
    public interface IStorageService: IStorage
    {
        public string StorageName { get;  }
    }
}
