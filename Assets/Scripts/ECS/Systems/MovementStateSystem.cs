using ECS.Components;
using ECS.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

public sealed class MovementStateSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<StopMovementRequest>> _stopReq = WorldNames.EVENTS;
    private readonly EcsFilterInject<Inc<ResumeMovementRequest>> _resumeReq= WorldNames.EVENTS;
    private readonly EcsPoolInject<StopMovementTag> _stop;

    public void Run(IEcsSystems s)
    {
        foreach (var e in _stopReq.Value)
        {
            if (!_stop.Value.Has(e)) _stop.Value.Add(e);
        }

        foreach (var e in _resumeReq.Value)
        {
            if (_stop.Value.Has(e)) _stop.Value.Del(e);
        }
    }
}