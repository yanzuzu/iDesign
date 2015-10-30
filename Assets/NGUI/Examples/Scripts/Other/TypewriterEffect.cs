//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2014 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Trivial script that fills the label's contents gradually, as if someone was typing.
/// </summary>

[RequireComponent(typeof(UILabel))]
[AddComponentMenu("NGUI/Examples/Typewriter Effect")]
public class TypewriterEffect : MonoBehaviour
{
	public int charsPerSecond = 40;

	UILabel mLabel;
	string mText;
	int mOffset = 0;
	float mNextChar = 0f;
	bool mReset = true;
	// Ville Add
	public System.Action m_WriterTextCallback = null;
	public System.Action m_TextEndCallback = null;

	public bool m_IsFinish { get {
			if( mText == null ) return false;
			return (mOffset >= mText.Length); 
		}
	}

	void OnEnable () { mReset = true; }

	void Update ()
	{
		if (mReset)
		{
			mOffset = 0;
			mReset = false;
			mLabel = GetComponent<UILabel>();
			mText = mLabel.processedText;
		}

		if (mOffset < mText.Length && mNextChar <= RealTime.time)
		{
			charsPerSecond = Mathf.Max(1, charsPerSecond);

			// Periods and end-of-line characters should pause for a longer time.
			float delay = 1f / charsPerSecond;
			char c = mText[mOffset];
			if (c == '.' || c == '\n' || c == '!' || c == '?') delay *= 4f;
			// check has BBC code?
			if( c == '[')
			{
				for( int i = mOffset ; i < mText.Length ; i ++ )
				{
					char TmpChar = mText[i];
					if( TmpChar != ']' )
					{
						mOffset ++;

					}else
					{
						break;
					}
				}
			}
			if( mOffset >= mText.Length ) mOffset = mText.Length - 1;
			// Automatically skip all symbols
			NGUIText.ParseSymbol(mText, ref mOffset);

			mNextChar = RealTime.time + delay;
			mLabel.text = mText.Substring(0, ++mOffset);
			// Ville Add
			if( m_WriterTextCallback != null )
				m_WriterTextCallback();
		}
		if( mOffset == mText.Length )
		{
			if( m_TextEndCallback != null )
			{
				m_TextEndCallback();
				m_TextEndCallback = null;
			}
		}
	}
	// Ville Add
	public void Flush()
	{
		if( null == mText ) return;
		mOffset = mText.Length;
		mLabel.text = mText;
	}

	public void Init( string p_text )
	{
		if( mLabel == null )
		{
			mLabel = GetComponent<UILabel>();
		}
		mOffset = 0;
		mText = p_text;
	}

	public void SetTxt( string p_text )
	{
		mLabel.text = p_text;
	}
}
