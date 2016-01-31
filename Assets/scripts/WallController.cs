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
		wallSpriteObj = wallObj.GetComponent<UISprite> ();
		wallSpriteObj.depth = 1;
		wallSpriteObj.pivot = UIWidget.Pivot.Left;
		Vector3 wordPos = ServiceLocator< CameraManager >.Instance.GetUiCamera().ScreenToWorldPoint (new Vector3( pMousePos.x, pMousePos.y , 0 ));
		wallObj.transform.position = wordPos;

		wallObjStartLocalPos = wallObj.transform.localPosition;
	}

	public void ScaleWall(Vector2 startPosition, Vector2 moveMousePos)
	{
		float dist = Vector3.Distance (startPosition, moveMousePos);
		float deltaX = startPosition.y < moveMousePos.y ? 1 : -1;

		Vector2 dir = new Vector2 (moveMousePos.x - startPosition.x, moveMousePos.y - startPosition.y);

		float angle =  deltaX * Vector2.Angle (new Vector2(1,0),dir);

		wallObj.transform.localScale = new Vector3 ( dist/10f, 1, 1);
		wallObj.transform.localEulerAngles = new Vector3 (0, 0, angle);
	}
}
