using System;
using System.Collections.Generic;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.EventHandler;
using Infrastructure.Services.Identifiers;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;

namespace Infrastructure.State
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> states;
        private IExitableState activeState;

        public GameStateMachine(SceneLoader sceneLoader, AllServices services)
        {
            states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, services.Single<IGameFactory>(),
                    services.Single<IPersistentProgressService>(), services.Single<IGameEventHandlerService>(),
                    services.Single<IInputService>(), services.Single<ISaveLoadService>(),
                    services.Single<IIdentifierService>()),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(),
                    services.Single<ISaveLoadService>()),
                [typeof(GameLoopState)] = new GameLoopState(this),
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