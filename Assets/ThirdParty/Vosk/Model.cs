namespace Vosk
{
    public class Model : System.IDisposable
    {
        private System.Runtime.InteropServices.HandleRef handle;

        internal Model(System.IntPtr cPtr)
        {
            handle = new System.Runtime.InteropServices.HandleRef(this, cPtr);
        }

        internal static System.Runtime.InteropServices.HandleRef getCPtr(Model obj)
        {
            return (obj == null)
                ? new System.Runtime.InteropServices.HandleRef(null, System.IntPtr.Zero)
                : obj.handle;
        }

        ~Model()
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
                    VoskPINVOKE.delete_Model(handle);
                    handle = new System.Runtime.InteropServices.HandleRef(null, System.IntPtr.Zero);
                }
            }
        }

        public Model(string model_path) : this(VoskPINVOKE.new_Model(model_path))
        {
        }

        public int vosk_model_find_word(string word)
        {
            return VoskPINVOKE.Model_vosk_model_find_word(handle, word);
        }
    }
}