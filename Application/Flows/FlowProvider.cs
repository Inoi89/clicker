using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Flows
{
    public class FlowProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public FlowProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<IFlow> GetFlows()
        {
            // Здесь создаем и возвращаем коллекцию всех потоков
            return new List<IFlow>
        {
            _serviceProvider.GetRequiredService<GamingFlow>(),
            _serviceProvider.GetRequiredService<CombatFlow>()
        };
        }
    }
}
