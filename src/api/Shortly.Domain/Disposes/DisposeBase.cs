using System.Runtime.InteropServices;

namespace Shortly.Domain.Disposes
{
    public class DisposeBase : IDisposable
    {
        private bool IsDisposed;

        private IntPtr nativeResource = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(DisposeBase)));

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void CleanUp()
        {

        }

        // The bulk of clean code is implemented in Disposed(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed) { return; }
            if (disposing)
            {
                CleanUp();
            }

            // Free native-source if there are any
            if (nativeResource != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(nativeResource);
                nativeResource = IntPtr.Zero;
            }

            IsDisposed = true;
        }

        ~DisposeBase()
        {
            Dispose(false);
        }
    }
}
