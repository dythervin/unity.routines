using Dythervin.Updaters;
using UnityEngine;

namespace Dythervin.Routines
{
    public class WaitForSecondsInstr : IRoutineInstruction, IUpdatable, IUpdatableFixed
    {
        private float _elapsed;
        private bool _fixedUpdate;
        private float _seconds;
        private bool _unscaled;
        public WaitForSecondsInstr(float seconds, bool fixedUpdate = false, bool unscaled = false)
        {
            Init(seconds, fixedUpdate, unscaled);
        }

        public void Start()
        {
            SetUpdatable(true);
        }

        public void Stop()
        {
            _elapsed = 0;
            Pause();
        }

        public void Pause()
        {
            SetUpdatable(false);
        }

        IInstructionDoneListener IRoutineInstruction.DoneListener { get; set; }

        public IRoutineInstruction GetCopy()
        {
            return new WaitForSecondsInstr(_seconds, _fixedUpdate, _unscaled) { _elapsed = _elapsed };
        }

        void IUpdatable.OnUpdate()
        {
            MoveNext(_unscaled ? Time.unscaledDeltaTime : Time.deltaTime);
        }

        void IUpdatableFixed.OnUpdate()
        {
            MoveNext(_unscaled ? Time.fixedUnscaledDeltaTime : Time.fixedDeltaTime);
        }

        private void SetUpdatable(bool value)
        {
            if (_fixedUpdate)
                this.SetFixedUpdater(value);
            else
                this.SetUpdater(value);
        }

        public void Init(float seconds, bool fixedUpdate = false, bool unscaled = false)
        {
            _fixedUpdate = fixedUpdate;
            _unscaled = unscaled;
            _seconds = seconds;
            _elapsed = 0;
        }

        private void MoveNext(float deltaTime)
        {
            if (_elapsed >= _seconds)
                Done();
            else
                _elapsed += deltaTime;
        }

        private void Done()
        {
            Stop();
            ((IRoutineInstruction)this).DoneListener.OnDone();
        }
    }
}