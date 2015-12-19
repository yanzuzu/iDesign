using UnityEngine;
using System.Collections;
using ZuEngine;
using ZuEngine.Manager;


public class WallController {
	#region private memeber
	private GameObject wallObj;
	private UISprite wallSpriteObj;
	private Vector3 wallObjStartLocalPos;
	#endregion

	public void CreateWall(BuildData pBuildData, GameObject pParentObj , Vector2 pMousePos)
	{
		wallObj = ServiceLocator<ResourceManager>.Instance.LoadRes (pBuildData.ResourcePath, true);
		wallObj.transform.SetParent (pParentObj.transform);
		wallObj.transform.localScale = Vector3.one;
		wallObj.GetComponent<UISprite> ().depth = 1;
		Vector3 wordPos = ServiceLocator< CameraManager >.Instance.GetUiCamera().ScreenToWorldPoint (new Vector3( pMousePos.x, pMousePos.y , 0 ));
		wallObj.transform.position = wordPos;
		wallSpriteObj = wallObj.GetComponent<UISprite> ();
		wallObjStartLocalPos = wallObj.transform.localPosition;
	}

	public void ScaleWall(Vector2 startPosition, Vector2 moveMousePos)
	{
		//Vector3 startPos = ServiceLocator< CameraManager >.Instance.GetUiCamera().ScreenToWorldPoint (new Vector3( startPosition.x, startPosition.y , 0 ));
		//Vector3 movePos = ServiceLocator< CameraManager >.Instance.GetUiCamera().ScreenToWorldPoint (new Vector3( moveMousePos.x, moveMousePos.y , 0 ));
		float dist = Vector3.Distance (startPosition, moveMousePos);
		float deltaX = startPosition.x < moveMousePos.x ? 1 : -1;
		wallObj.transform.localPosition = wallObjStartLocalPos + new Vector3 ( deltaX * dist/2, 0, 0);
		wallSpriteObj.SetDimensions ((int)dist, 10);
	}
}
