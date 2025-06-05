using SiliFish.Database;
using SiliFish.Services;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace SiliFish.Repositories
{
    public class DatabaseDumper : IDisposable
    {
        private readonly string dbName;
        private readonly ConcurrentQueue<object> Queue = [];
        private bool isDisposed;
        private readonly CancellationTokenSource cancellationTokenSource = new();

        public bool HasToDump() => !Queue.IsEmpty;

        private async Task RunLoop()
        {
            while (!cancellationTokenSource.IsCancellationRequested)
            {
                if (!Queue.IsEmpty)
                    await Flush();
                else
                    await Task.Delay(100);
            }
            await Flush(true);
        }

        private async Task Flush(bool finalDump = false)
        {
            try
            {
                using SFDataContext dataContext = new(dbName);
                int counter = 0;
                while (finalDump || counter++ < 100)
                {
                    if (!Queue.TryDequeue(out var nextToDump))
                        break;
                    if (nextToDump is IEnumerable dumps)
                    {
                        foreach (var item in dumps)
                            dataContext.Add(item);
                    }
                    else
                        dataContext.Add(nextToDump);
                }
                await dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        public DatabaseDumper(string dbName)
        {
            this.dbName = dbName;
            _ = Task.Run(RunLoop);
        }
        public void Dump(object record)
        {
            Queue.Enqueue(record);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                    cancellationTokenSource.Cancel();
                isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
