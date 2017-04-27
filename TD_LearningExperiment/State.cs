using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD_LearningExperiment
{
    public class State
    {
        public string Name { get; private set; }
        public List<AfterStateAction> AvailableStateRewards { get; private set; } = new List<AfterStateAction>();

        public State(string name)
        {
            Name = name;
        }
    }
}
