using UnityEngine;
using System.Collections;
using ZuEngine.StateManagement;
using ZuEngine;

public class MainGameState : GameState {

	private long STATE_PLAY;

	public MainGameState( StateManager p_stateMgr ) : base( p_stateMgr )
	{
		ZuDebug.Log ("####### MainGameState start #########");
		InitState ();
		InitTask ();
	}

	private void InitState()
	{
		STATE_PLAY = TaskManager.CreateState ();
	}

	private void InitTask()
	{
		TaskManager.AddTask (new FurnitureTask (), STATE_PLAY);
		TaskManager.AddTask (new FurnitureSaveTask (), STATE_PLAY);
	}
	
	#region implement of GameState
	public override void Init()
	{
		TaskManager.ChangeState (STATE_PLAY);
	}
	
	public override void Update(float deltaTime)
	{
		base.Update (deltaTime);

	}
	
	#endregion
}
