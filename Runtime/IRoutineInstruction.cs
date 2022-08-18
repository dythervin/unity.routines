namespace Dythervin.Routines
{
    public interface IRoutineInstruction
    {
        void Start();
        void Stop();
        void Pause();
        IInstructionDoneListener DoneListener { get; internal set; }
        IRoutineInstruction GetCopy();
    }
}