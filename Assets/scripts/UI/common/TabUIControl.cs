using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TabUIControl : MonoBehaviour {
	[SerializeField]
	private List<UIButton> m_tabBtns = new List<UIButton>();
	
	void Awake()
	{
		for( int i = 0 ; i < m_tabBtns.Count ; i ++ )
		{
			int pIdx = i;
			EventDelegate.Set(m_tabBtns[i].onClick, delegate {
				onClickTabBtn(pIdx);	
			});
		}
	}

	public void SetTabActiveIdx( int pIdx )
	{
		for( int i = 0 ; i < m_tabBtns.Count ; i ++ )
		{
			m_tabBtns[i].isEnabled = ( i != pIdx );
		}
	}

	private void onClickTabBtn(int userData)
	{
		ZuDebug.Log ("OnClicktTabBtn userData = " +  userData);
		SetTabActiveIdx (userData);
	}
}
