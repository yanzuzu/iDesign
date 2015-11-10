using UnityEngine;
using System.Collections;

public class HistoryNode : MonoBehaviour {
	[SerializeField]
	private MyUILabel m_nodeTxt;
	[SerializeField]
	private BoxCollider m_collid;

	public void Setup(string showName)
	{
		m_nodeTxt.text = showName;
	}

	public void Resize(int reSize)
	{
		m_collid.size = new Vector3 (reSize, m_collid.bounds.size.y, m_collid.bounds.size.z);
	}

	void OnClickHitoryNode()
	{
		ZuDebug.Log (string.Format("OnClickHistoyNode name = {0}",m_nodeTxt.text));
	}
}
