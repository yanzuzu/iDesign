using UnityEngine;
using System.Collections;
using ZuEngine;
using ZuEngine.Manager;


public class WallController {
	private GameObject wallObj;

	public void CreateWall(BuildData pBuildData, GameObject pParentObj , Vector2 pMousePos)
	{
		wallObj = ServiceLocator<ResourceManager>.Instance.LoadRes (pBuildData.ResourcePath, true);
		wallObj.transform.SetParent (pParentObj.transform);
		wallObj.transform.localScale = Vector3.one;
		wallObj.GetComponent<UISprite> ().depth = 1;
		Vector3 wordPos = ServiceLocator< CameraManager >.Instance.GetUiCamera().ScreenToWorldPoint (new Vector3( pMousePos.x, pMousePos.y , 0 ));
		wallObj.transform.position = wordPos;
	}

	public void ScaleWall(Vector2 moveMousePos)
	{

	}
}
