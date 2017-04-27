namespace TD_LearningExperiment
{
    public struct TransitionAction
    {
        public Transition transition;
        public double reward;

        public TransitionAction(Transition transition, double reward)
        {
            this.transition = transition;
            this.reward = reward;
        }
    }
    public struct AfterStateAction
    {
        public State afterState;
        public double reward;

        public AfterStateAction(State afterState, double reward)
        {
            this.afterState = afterState;
            this.reward = reward;
        }
    }
}
