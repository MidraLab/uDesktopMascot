namespace Vosk
{
    class VoskPINVOKE
    {
        static VoskPINVOKE()
        {
        }

        [System.Runtime.InteropServices.DllImport("libvosk", EntryPoint = "vosk_model_new")]
        public static extern System.IntPtr new_Model(string jarg1);

        [System.Runtime.InteropServices.DllImport("libvosk", EntryPoint = "vosk_model_free")]
        public static extern void delete_Model(System.Runtime.InteropServices.HandleRef jarg1);

        [System.Runtime.InteropServices.DllImport("libvosk", EntryPoint = "vosk_model_find_word")]
        public static extern int Model_vosk_model_find_word(System.Runtime.InteropServices.HandleRef jarg1,
            string jarg2);

        [System.Runtime.InteropServices.DllImport("libvosk", EntryPoint = "vosk_spk_model_new")]
        public static extern System.IntPtr new_SpkModel(string jarg1);

        [System.Runtime.InteropServices.DllImport("libvosk", EntryPoint = "vosk_spk_model_free")]
        public static extern void delete_SpkModel(System.Runtime.InteropServices.HandleRef jarg1);

        [System.Runtime.InteropServices.DllImport("libvosk", EntryPoint = "vosk_recognizer_new")]
        public static extern System.IntPtr new_VoskRecognizer(
            System.Runtime.InteropServices.HandleRef jarg1, float jarg2);

        [System.Runtime.InteropServices.DllImport("libvosk", EntryPoint = "vosk_recognizer_new_spk")]
        public static extern System.IntPtr new_VoskRecognizerSpk(
            System.Runtime.InteropServices.HandleRef jarg1, float jarg2,
            System.Runtime.InteropServices.HandleRef jarg3);

        [System.Runtime.InteropServices.DllImport("libvosk", EntryPoint = "vosk_recognizer_new_grm")]
        public static extern System.IntPtr new_VoskRecognizerGrm(
            System.Runtime.InteropServices.HandleRef jarg1, float jarg2, string jarg3);

        [System.Runtime.InteropServices.DllImport("libvosk", EntryPoint = "vosk_recognizer_free")]
        public static extern void delete_VoskRecognizer(System.Runtime.InteropServices.HandleRef jarg1);

        [System.Runtime.InteropServices.DllImport("libvosk",
            EntryPoint = "vosk_recognizer_set_max_alternatives")]
        public static extern void VoskRecognizer_SetMaxAlternatives(
            System.Runtime.InteropServices.HandleRef jarg1, int jarg2);

        [System.Runtime.InteropServices.DllImport("libvosk", EntryPoint = "vosk_recognizer_set_words")]
        public static extern void VoskRecognizer_SetWords(System.Runtime.InteropServices.HandleRef jarg1,
            int jarg2);

        [System.Runtime.InteropServices.DllImport("libvosk", EntryPoint = "vosk_recognizer_set_spk_model")]
        public static extern void VoskRecognizer_SetSpkModel(System.Runtime.InteropServices.HandleRef jarg1,
            System.Runtime.InteropServices.HandleRef jarg2);

        [System.Runtime.InteropServices.DllImport("libvosk", EntryPoint = "vosk_recognizer_accept_waveform")]
        public static extern bool VoskRecognizer_AcceptWaveform(System.Runtime.InteropServices.HandleRef jarg1,
            [System.Runtime.InteropServices.In,
             System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType
                 .LPArray)]
            byte[] jarg2, int jarg3);

        [System.Runtime.InteropServices.DllImport("libvosk", EntryPoint = "vosk_recognizer_accept_waveform_s")]
        public static extern bool VoskRecognizer_AcceptWaveformShort(
            System.Runtime.InteropServices.HandleRef jarg1,
            [System.Runtime.InteropServices.In,
             System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType
                 .LPArray)]
            short[] jarg2, int jarg3);

        [System.Runtime.InteropServices.DllImport("libvosk", EntryPoint = "vosk_recognizer_accept_waveform_f")]
        public static extern bool VoskRecognizer_AcceptWaveformFloat(
            System.Runtime.InteropServices.HandleRef jarg1,
            [System.Runtime.InteropServices.In,
             System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType
                 .LPArray)]
            float[] jarg2, int jarg3);

        [System.Runtime.InteropServices.DllImport("libvosk", EntryPoint = "vosk_recognizer_result")]
        public static extern System.IntPtr VoskRecognizer_Result(
            System.Runtime.InteropServices.HandleRef jarg1);

        [System.Runtime.InteropServices.DllImport("libvosk", EntryPoint = "vosk_recognizer_partial_result")]
        public static extern System.IntPtr VoskRecognizer_PartialResult(
            System.Runtime.InteropServices.HandleRef jarg1);

        [System.Runtime.InteropServices.DllImport("libvosk", EntryPoint = "vosk_recognizer_final_result")]
        public static extern System.IntPtr VoskRecognizer_FinalResult(
            System.Runtime.InteropServices.HandleRef jarg1);

        [System.Runtime.InteropServices.DllImport("libvosk", EntryPoint = "vosk_recognizer_reset")]
        public static extern void VoskRecognizer_Reset(System.Runtime.InteropServices.HandleRef jarg1);

        [System.Runtime.InteropServices.DllImport("libvosk", EntryPoint = "vosk_set_log_level")]
        public static extern void SetLogLevel(int jarg1);

        [System.Runtime.InteropServices.DllImport("libvosk", EntryPoint = "vosk_gpu_init")]
        public static extern void GpuInit();

        [System.Runtime.InteropServices.DllImport("libvosk", EntryPoint = "vosk_gpu_thread_init")]
        public static extern void GpuThreadInit();
    }
}