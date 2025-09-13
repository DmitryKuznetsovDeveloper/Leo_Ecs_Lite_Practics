namespace ECS.Services
{
    public interface IPoolableProvider
    {
        void SetupPool(IGameObjectPool pool);
        void ReturnToPool();
    }
    
    public interface IGameObjectPool
    {
        void Return(EntityProvider instance);
    }
}