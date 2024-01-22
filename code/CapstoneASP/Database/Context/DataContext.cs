using webapi.Database.Context;

namespace CapstoneASP.Database.Context
{
    public interface IDataContext
    {
        #region Methods

        Task<IDbConnectionWrapper> CreateConnection();

        void Init();

        #endregion
    }

    public class DataContext : IDataContext
    {
        public Task<IDbConnectionWrapper> CreateConnection()
        {
            throw new NotImplementedException();
        }

        public void Init()
        {
            throw new NotImplementedException();
        }
    }
}
