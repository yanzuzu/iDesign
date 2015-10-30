using UnityEngine;
using System.Collections;

/// <summary>
/// If you want to animate the UIWidget's color with Unity's animation/animator, please add this script, and control the "color" property in this script.
/// </summary>
[RequireComponent(typeof(UIWidget))]
public class UIWidgetColorModifier : MonoBehaviour 
{
    [SerializeField]
    private Color m_color;
    private UIWidget m_widget;
    private void Awake()
    {
        m_widget = GetComponent<UIWidget>();
        m_color = m_widget.color;
    }
	
	// Update is called once per frame
	void Update () 
    {
        m_widget.color = m_color;
	}
}
