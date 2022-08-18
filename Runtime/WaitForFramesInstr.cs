using Dythervin.Updater;

namespace Dythervin.Routines
{
    public class WaitForFramesInstr : IRoutineInstruction, IUpdatable, IUpdatableFixed
    {
        private int _elapsed;
        private bool _fixedUpdate;
        private int _frames;

        public WaitForFramesInstr() { }

        public WaitForFramesInstr(int frames, bool fixedUpdate = false)
        {
            Init(frames, fixedUpdate);
        }

        private bool Updatable
        {
            get => _fixedUpdate ? this.GetFixedUpdater() : this.GetUpdater();
            set
            {
                if (_fixedUpdate)
                    this.SetFixedUpdater(value);
                else
                    this.SetUpdater(value);
            }
        }

        public void Start()
        {
            Updatable = true;
        }

        public void Stop()
        {
            _elapsed = 0;
            Pause();
        }

        public void Pause()
        {
            Updatable = false;
        }

        IInstructionDoneListener IRoutineInstruction.DoneListener { get; set; }

        public IRoutineInstruction GetCopy()
        {
            return new WaitForFramesInstr(_frames, _fixedUpdate) { _elapsed = _elapsed };
        }

        void IUpdatable.OnUpdate()
        {
            MoveNext();
        }
        void IUpdatableFixed.OnUpdate()
        {
            MoveNext();
        }

        public void Init(int frames, bool fixedUpdate = false)
        {
            _fixedUpdate = fixedUpdate;
            _frames = frames;
            _elapsed = 0;
        }

        private void MoveNext()
        {
            if (_elapsed >= _frames)
                Done();
            else
                _elapsed++;
        }

        private void Done()
        {
            Stop();
            ((IRoutineInstruction)this).DoneListener.OnDone();
        }
    }
}