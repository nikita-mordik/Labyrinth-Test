using FreedLOW._Maze.Scripts.Infrastructure.Services;

namespace FreedLOW._Maze.Scripts.Infrastructure.State
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    }
}