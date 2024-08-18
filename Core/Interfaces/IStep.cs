using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IStep
    {
        void Execute();
        bool IsCompleted { get; }
        void Reset();
    }

    public abstract class BaseStep : IStep
    {
        public bool IsCompleted { get; protected set; }

        public abstract void Execute();
        public virtual void Reset()
        {
            IsCompleted = false;
        }
    }
}
