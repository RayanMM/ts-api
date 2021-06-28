using System;
using System.Data;
using System.Diagnostics;

namespace TS.Infra.Context
{
    public class DbContext : IDisposable
    {
        private readonly Lazy<IDbConnection> connectionLazy;

        public IDbConnection Connection => connectionLazy.Value;

        public DbContext(IDbConnection connection)
        {
            connectionLazy = new Lazy<IDbConnection>(() =>
            {
                return OpenConnectionService(connection);
            });
        }

        private IDbConnection OpenConnectionService(IDbConnection conn)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

            }catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return conn;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (connectionLazy.IsValueCreated)
                    Connection.Dispose();
            }
        }

        ~DbContext()
        {
            Dispose(false);
        }
    }
}
