using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD_LearningExperiment
{
    class Program
    {
        static void ShowValueFunction(Dictionary<Transition, double> valueFunction)
        {
            Console.WriteLine("==========ValueFunction: ====================");
            foreach (var row in valueFunction)
            {
                Console.WriteLine($"\tS,A: {row.Key.originState} to {row.Key.afterState}, value: {row.Value}");
            }
            Console.WriteLine("============================================");
        }

        static void Main(string[] args)
        {
            Dictionary<Transition, double> valueFunction = new Dictionary<Transition, double>();

            State a = new State("A"), b = new State("B"), c = new State("C"), d = new State("D"), e = new State("E"), f = new State("F");
            a.AvailableStateRewards.Add(new AfterStateAction(b, 0));
            b.AvailableStateRewards.Add(new AfterStateAction(c, 0));
            b.AvailableStateRewards.Add(new AfterStateAction(d, 0));
            c.AvailableStateRewards.Add(new AfterStateAction(b, 0));
            d.AvailableStateRewards.Add(new AfterStateAction(f, 100));
            d.AvailableStateRewards.Add(new AfterStateAction(e, 0));
            e.AvailableStateRewards.Add(new AfterStateAction(d, 0));

            List<State> allStates = new List<State> { a, b, c, d, e, f };
            foreach(var state in allStates)
            {
                foreach(var action in state.AvailableStateRewards)
                {
                    valueFunction.Add(new Transition(state, action.afterState), 0);
                }
            }
            Random randomGenerator = new Random(Guid.NewGuid().GetHashCode());
            //Q_LearningAgent agent = new Q_LearningAgent(0.5);
            AdvanceQ_LearningAgent agent = new AdvanceQ_LearningAgent(0.5);
            int totalStepCount = 0;
            for (int i = 0; i < 2000; i++)
            {
                List<TransitionAction> path = new List<TransitionAction>();
                State currentState = a;
                while(currentState.AvailableStateRewards.Count != 0)
                {
                    TransitionAction bestAction;
                    if (randomGenerator.NextDouble() < 0.25)
                    {
                        var action = currentState.AvailableStateRewards[randomGenerator.Next(currentState.AvailableStateRewards.Count)];
                        bestAction = new TransitionAction(new Transition(currentState, action.afterState), action.reward);
                    }
                    else
                    {
                        bestAction = agent.GetBestAction(currentState, valueFunction);
                    }
                    path.Add(new TransitionAction(new Transition(bestAction.transition.originState, bestAction.transition.afterState), bestAction.reward));
                    currentState = bestAction.transition.afterState;
                }
                Console.WriteLine($"Episode: {i} step: {path.Count}");
                //if(i >= 1000)
                    totalStepCount += path.Count;
                //path.ForEach(x =>
                //{
                //    Console.WriteLine($"\t {x.Item1.Name} {x.Item2.Name} to {x.Item3.Name}, reward: {x.Item4}");
                //});
                agent.Train(path, valueFunction);
            }
            ShowValueFunction(valueFunction);
            Console.WriteLine(totalStepCount);
            Console.ReadLine();
        }
    }
}
