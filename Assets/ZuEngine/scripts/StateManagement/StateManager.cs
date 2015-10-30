using System;

namespace ZuEngine.StateManagement
{
	public class StateManager : IUpdatable
	{
		public bool IsPaused { get; set; }
		public GameState CurrentState { get; private set; }


		public StateManager()
		{
			IsPaused = false;
			CurrentState = null;
		}


		public void ChangeState(GameState newState)
		{
			if(CurrentState != null)
			{
				CurrentState.Destroy();
			}

			CurrentState = newState;
		}


		public void Update(float deltaTime)
		{
			if(CurrentState != null && !IsPaused)
			{
				if(!CurrentState.HasBeenInited)
				{
					CurrentState.Init();
					CurrentState.HasBeenInited = true;
				}

				CurrentState.Update(deltaTime);
			}
		}
	}
}

