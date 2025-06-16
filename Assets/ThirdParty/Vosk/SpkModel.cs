namespace Vosk
{
    public class SpkModel : System.IDisposable
    {
        private System.Runtime.InteropServices.HandleRef handle;

        internal SpkModel(System.IntPtr cPtr)
        {
            handle = new System.Runtime.InteropServices.HandleRef(this, cPtr);
        }

        internal static System.Runtime.InteropServices.HandleRef getCPtr(SpkModel obj)
        {
            return (obj == null)
                ? new System.Runtime.InteropServices.HandleRef(null, System.IntPtr.Zero)
                : obj.handle;
        }

        ~SpkModel()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            lock (this)
            {
                if (handle.Handle != System.IntPtr.Zero)
                {
                    VoskPINVOKE.delete_SpkModel(handle);
                    handle = new System.Runtime.InteropServices.HandleRef(null, System.IntPtr.Zero);
                }
            }
        }

        public SpkModel(string model_path) : this(VoskPINVOKE.new_SpkModel(model_path))
        {
        }
    }
}