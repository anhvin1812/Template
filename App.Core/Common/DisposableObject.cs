using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Common
{
    public abstract class DisposableObject : IDisposable
    {
        protected DisposableObject()
        {
            Disposables = new List<IDisposable>();
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (_isDisposed) return;

            _isDisposed = true;

            if (isDisposing)
            {
                foreach (var disposable in Disposables)
                {
                    disposable.Dispose();
                }
            }

            Disposables = null;
        }

        private void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        void IDisposable.Dispose()
        {
            Dispose();
        }

        private bool _isDisposed = false;

        protected IList<IDisposable> Disposables { get; private set; }
    }
}
