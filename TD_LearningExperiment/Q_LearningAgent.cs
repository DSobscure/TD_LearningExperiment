using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD_LearningExperiment
{
    public class Q_LearningAgent
    {
        public double LearningRate { get; private set; }

        public Q_LearningAgent(double learningRate)
        {
            LearningRate = learningRate;
        }

        public TransitionAction GetBestAction(State currentState, Dictionary<Transition, double> valueFunction)
        {
            double maxScore = -1;
            TransitionAction bestAction = default(TransitionAction);
            foreach (var action in currentState.AvailableStateRewards)
            {
                double score = valueFunction[new Transition(currentState, action.afterState)];
                if (score > maxScore)
                {
                    bestAction = new TransitionAction(new Transition(currentState, action.afterState), action.reward);
                    maxScore = score;
                }
                else if (score == maxScore)
                {
                    Random random = new Random(Guid.NewGuid().GetHashCode());
                    if (random.NextDouble() < 0.5)
                    {
                        bestAction = new TransitionAction(new Transition(currentState, action.afterState), action.reward);
                        maxScore = score;
                    }
                }
            }
            return bestAction;
        }

        public void Train(List<TransitionAction> path, Dictionary<Transition, double> valueFunction)
        {
            foreach (var action in path)
            {
                double oldValue = valueFunction[action.transition];
                double newValue;
                if (action.transition.afterState.AvailableStateRewards.Count == 0)
                {
                    newValue = action.reward;
                }
                else
                {
                    TransitionAction bestAction = GetBestAction(action.transition.afterState, valueFunction);
                    newValue = valueFunction[bestAction.transition] + action.reward;
                }
                valueFunction[action.transition] = oldValue + LearningRate * (newValue - oldValue);
            }
        }
    }
}
