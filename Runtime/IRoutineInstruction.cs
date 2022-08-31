namespace Dythervin.Routines
{
    public interface IRoutineInstruction
    {
        void Start();
        void Stop();
        void Pause();
        IInstructionDoneListener DoneListener
        {
            get;

#if UNITY_2021_3_OR_NEWER
            protected internal
#endif
            set;
        }
        IRoutineInstruction GetCopy();
    }
}