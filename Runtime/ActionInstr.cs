using System;

namespace Dythervin.Routines
{
    public class ActionInstr : IRoutineInstruction
    {
        public ActionInstr(Action action = null)
        {
            Init(action);
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

    public class ActionInstr<T> : IRoutineInstruction
    {
        public T value;
        public ActionInstr(T value, Action<T> action = null)
        {
            Init(action, value);
        }


        void IRoutineInstruction.Start()
        {
            action.Invoke(value);
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
            var obj = new ActionInstr<T>(value, action);
            return obj;
        }

        public event Action<T> action;

        public void Init(Action<T> action, T value)
        {
            this.action = action;
            this.value = value;
        }
    }
}