using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grippable : MonoBehaviour
{
	private bool m_canGrip = false;

	static List<Grippable> s_inputListeners;

	static Grippable()
	{
		s_inputListeners = new List<Grippable>();
	}

	public static void GripButtonPressed()
	{
		foreach ( var grippable in s_inputListeners )
		{
			grippable.OnGrip();
		}
	}

	public static void GripButtonReleased()
	{
		foreach ( var grippable in s_inputListeners )
		{
			grippable.OnRelease();
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		// Probably want to change this to only listen when trigger area overlap
		s_inputListeners.Add( this );
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void OnGrip()
	{
		QuestDebug.ConsoleLog( "Gripped: " + transform.name );
	}

	public void OnRelease()
	{
		QuestDebug.ConsoleLog( "Released: " + transform.name );
	}

	public void OnTriggerEnter( Collider other )
	{
		if ( other.transform.GetComponent<XRHandController>() != null )
		{
			m_canGrip = true;
		}
	}

	public void OnTriggerExit( Collider other )
	{
		if ( other.transform.GetComponent<XRHandController>() != null )
		{
			m_canGrip = false;
		}
	}
}
