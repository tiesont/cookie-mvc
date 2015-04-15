using System;

namespace CookieMVC.ApplicationServices
{
    public class ServiceBase : IDisposable
    {
        private bool disposed = false; // to detect redundant calls
        private Data.DataContext context;

        public Data.DataContext Context
        {
            get
            {
                if (context == null)
                {
                    context = new Data.DataContext();
                }

                return context;
            }
            private set
            {
                context = value;
            }
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    if (context != null)
                    {
                        context.Dispose();
                        context = null;
                    }
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
