using System;
using System.Collections.Generic;
using FreedLOW._Maze.Scripts.Infrastructure.Factory;
using FreedLOW._Maze.Scripts.Infrastructure.Services;
using FreedLOW._Maze.Scripts.Infrastructure.Services.EventHandler;
using FreedLOW._Maze.Scripts.Infrastructure.Services.Identifiers;
using FreedLOW._Maze.Scripts.Infrastructure.Services.Input;
using FreedLOW._Maze.Scripts.Infrastructure.Services.PersistentProgress;
using FreedLOW._Maze.Scripts.Infrastructure.Services.SaveLoad;
using FreedLOW._Maze.Scripts.Infrastructure.State.States;

namespace FreedLOW._Maze.Scripts.Infrastructure.State
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> states;
        private IExitableState activeState;

        public GameStateMachine(ICoroutineRunner coroutine, SceneLoader sceneLoader, AllServices services)
        {
            states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(),
                    services.Single<ISaveLoadService>()),
                [typeof(LoadMenuState)] = new LoadMenuState(this, sceneLoader, services.Single<IGameFactory>()),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, services.Single<IGameFactory>(),
                    services.Single<IPersistentProgressService>(), services.Single<IGameEventHandlerService>(),
                    services.Single<IInputService>(), services.Single<ISaveLoadService>(),
                    services.Single<IIdentifierService>(), coroutine),
                [typeof(GameLoopState)] = new GameLoopState(this, services.Single<IPersistentProgressService>()),
                [typeof(PauseState)] = new PauseState(this),
            };
        }
        
        public void Enter<TState>() where TState : class, IState
        {
            var currentState = ChangeState<TState>();
            currentState.Enter();
        }
        
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            var currentState = ChangeState<TState>();
            currentState.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            activeState?.Exit();
            
            TState currentState = GetState<TState>();
            activeState = currentState;
            
            return currentState;
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            states[typeof(TState)] as TState;
    }
}