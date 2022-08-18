using System;

namespace Dythervin.Routines
{
    public class ActionInstr : IRoutineInstruction
    {
        public ActionInstr(Action action = null)
        {
            this.action = action;
        }


        void IRoutineInstruction.Start()
        {
            action.Invoke();
            ((IRoutineInstruction)this).DoneListener.OnDone();
        }

        void IRoutineInstruction.Stop()
        {
            throw new NotImplementedException();
        }

        void IRoutineInstruction.Pause()
        {
            throw new NotImplementedException();
        }

        IInstructionDoneListener IRoutineInstruction.DoneListener { get; set; }

        public IRoutineInstruction GetCopy()
        {
            var obj = new ActionInstr(action);
            return obj;
        }

        public event Action action;

        public void Init(Action action)
        {
            this.action = action;
        }
    }
}