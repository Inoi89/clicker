using Application.Flows;
using Application.Services;
using Core.Interfaces;
using System.Drawing;
using UI.UIElements;

public class MainService
{
    private readonly IEnumerable<IFlow> _flows;

    public MainService(FlowProvider flowProvider)
    {
        _flows = flowProvider.GetFlows().OrderBy(flow => flow.Priority).ToList();
    }

    public void Start()
    {
        foreach (var flow in _flows)
        {
            if (!flow.IsCompleted || flow.ShouldRepeat)
            {
                flow.Execute();
            }
        }
    }
}
