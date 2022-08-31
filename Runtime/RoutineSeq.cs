using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace Dythervin.Routines
{
    public class RoutineSeq : IInstructionDoneListener, IRoutineInstruction
    {
        private readonly List<IRoutineInstruction> _instructions;
        private int _current;

        public int repeats;

        public bool IsLooping
        {
            get => repeats == int.MaxValue;
            private set => repeats = value ? int.MaxValue : 0;
        }

        public IRoutineInstruction Current => _instructions[_current];

        public RoutineSeq(int repeats) : this()
        {
            this.repeats = repeats;
        }

        public RoutineSeq(bool loop) : this()
        {
            IsLooping = loop;
        }

        public RoutineSeq()
        {
            _instructions = new List<IRoutineInstruction>();
        }

        void IInstructionDoneListener.OnDone()
        {
            Next();
        }

        public void Append([NotNull] IRoutineInstruction instruction)
        {
            if (this == instruction)
                throw new Exception("Cannot append itself");

            _instructions.Add(instruction);
            instruction.DoneListener = this;
        }

        public void SetCurrent(int index)
        {
            Stop();
            Assert.IsTrue(index >= 0 && index < _instructions.Count);
            _current = index;
        }

        public void Append(Action action)
        {
            Append(new ActionInstr(action));
        }

        public void Start()
        {
            Current.Start();
        }

        public void Pause()
        {
            Current.Pause();
        }

        IInstructionDoneListener IRoutineInstruction.DoneListener { get; set; }

        IRoutineInstruction IRoutineInstruction.GetCopy()
        {
            return GetCopy();
        }

        public void Stop()
        {
            Current.Stop();
        }

        private void Next()
        {
            if (++_current >= _instructions.Count)
            {
                _current = 0;
                if (!IsLooping && --repeats < 0)
                {
                    ((IRoutineInstruction)this).DoneListener?.OnDone();
                    return;
                }
            }

            Start();
        }

        public RoutineSeq GetCopy()
        {
            var other = new RoutineSeq(repeats);
            foreach (IRoutineInstruction instruction in _instructions)
                other.Append(instruction.GetCopy());

            other._current = _current;
            return other;
        }
    }
}