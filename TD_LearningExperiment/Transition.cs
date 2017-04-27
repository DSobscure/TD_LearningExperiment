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
    }
}
