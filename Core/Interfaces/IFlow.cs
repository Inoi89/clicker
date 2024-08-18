using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IFlow
    {
        void Execute();
        bool IsCompleted { get; }
        int Priority { get; set; }
        bool ShouldRepeat { get; set; }
        void Reset();
    }

    public abstract class BaseFlow : IFlow
    {
        protected List<IStep> Steps = new List<IStep>();
        public int Priority { get; set; }
        public bool IsCompleted { get; protected set; }
        public bool ShouldRepeat { get; set; }

        public void Execute()
        {
            foreach (var step in Steps)
            {
                if (!step.IsCompleted)
                {
                    step.Execute();

                    // Если шаг в потоке отъебнул с ошибкой - следующий не выполняется
                    if (!step.IsCompleted)
                    {
                        break;
                    }

                }
            }

            IsCompleted = Steps.All(step => step.IsCompleted);
        }

        public void Reset()
        {
            IsCompleted = false;
            foreach (var step in Steps)
            {
                step.Reset(); 
            }
        }
    }
}
