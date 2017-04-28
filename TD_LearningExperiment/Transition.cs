using System;

namespace TD_LearningExperiment
{
    public struct Transition
    {
        public State originState;
        public State afterState;

        public Transition(State originState, State afterState)
        {
            this.originState = originState;
            this.afterState = afterState;
        }

        public override bool Equals(object obj)
        {
            if(obj is Transition)
            {
                Transition other = (Transition)obj;
                return originState == other.originState && afterState == other.afterState;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return (originState.Name + afterState.Name).GetHashCode();
        }
    }
}
