using UnityEngine;
using Ink.Runtime;

namespace Methodyca.Core
{
    public class TestStory : InkCharStory
    {
        public bool do1, do2;


        protected override void CheckVariables()
        {
            inkStory.variablesState["do1"] = do1;

            inkStory.variablesState["do2"] = do2;
        }
    }
}