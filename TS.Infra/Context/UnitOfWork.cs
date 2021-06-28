using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TS.Infra.Context
{
    public class UnitOfWork
    {
        private readonly DbContext context;

        internal IDbTransaction Transaction { get; set; }

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public void BeginTrans()
        {
            Transaction = context.Connection.BeginTransaction();
        }

        public void Commit()
        {
            if(Transaction != null)
            {
                try
                {
                    Transaction.Commit();
                }catch(Exception e)
                {
                    Transaction.Rollback();
                    throw e;
                }
                finally
                {
                    Transaction.Dispose();
                    Transaction = null;
                }
            }
        }

        public void Rollback()
        {
            if(Transaction != null)
            {
                try
                {
                    Transaction.Rollback();
                }catch(Exception e)
                {
                    throw e;
                }
                finally
                {
                    Transaction.Dispose();
                    Transaction = null;
                }
            }
        }
    }
}
