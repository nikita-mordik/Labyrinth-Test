namespace FreedLOW._Maze.Scripts.Infrastructure.Services
{
    public class AllServices
    {
        #region Singelton

        private static AllServices instance;
        public static AllServices Container => instance ??= new AllServices();

        #endregion

        public void RegisterSingle<TService>(TService implementation) where TService : IService => 
            Implementation<TService>.ServiceInstance = implementation;

        public TService Single<TService>() where TService : IService => 
            Implementation<TService>.ServiceInstance;

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}