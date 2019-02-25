using System;
using Puppy.BLL;
using Puppy.Model.Data;
using Puppy.Model.Business;
using Puppy.Model.Message;
using Puppy.Model.Output;


namespace MyConcert.BLL
{
    public class MyConcertBase:IDisposable
    {
        #region Members
        private readonly BusinessStrategy _biz;
        private readonly DataConfiguration _dbConfig;
        private bool disposed = false;

        public BusinessStrategy BizObject
        {get{ return _biz;}}
        
        public DataConfiguration DbConfig
        {get{ return _dbConfig;}}

        #endregion
        
        public MyConcertBase()
        {
            _biz = new BusinessStrategy
                (
                    new IBusinessOperator[] 
                    {
                        new AddOperator(),
                        new GetOperator()
                    }
                );
            _dbConfig = new DataConfiguration();

            _dbConfig.DataProvider = DataConfiguration.GetDataProvider(ConfigBase.DATABASE_PROVIDER);
            _dbConfig.ConnectionString = ConfigBase.GetConnectionString();
            _dbConfig.DatabaseName = "myconcert";

        }

        public Result Add(object aConcert, IMessage msg)
        {
            return _biz.Execute(
                _dbConfig,
                BusinessOperator.Add,
                aConcert,
                msg
            );
        }

        public Result Get(string filter, IMessage msg)
        {
             return _biz.Execute(
                _dbConfig,
                BusinessOperator.Get,
                filter.ToString(),
                msg
            );
        }

         public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if(!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                // Note disposing has been done.
                disposed = true;

            }
        }

        ~MyConcertBase()
        {
            Dispose(false);
        }

    }
}