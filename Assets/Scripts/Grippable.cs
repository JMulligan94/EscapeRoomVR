using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grippable : MonoBehaviour
{
	private bool m_canGrip = false;

	static List<Grippable> s_inputListeners;

	public MeshRenderer m_mesh;

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
		if ( other.tag == "Hand" )
		{
			QuestDebug.Log( "OnTriggerEnter hit: " + transform.name + " - " + other.transform.name + "\nTags: " + other.tag + "\nMaterial: " + m_mesh.material.name );
			m_canGrip = true;
			List<string> outNames = new List<string>();
			QuestDebug.ConsoleLog( "Has highlighted: " + m_mesh.material.HasProperty( "IsHighlighted" ) );
			m_mesh.material.SetInt( "IsHighlighted", 1 );
		}
	}

	public void OnTriggerExit( Collider other )
	{
		if ( other.tag == "Hand" )
		{
			QuestDebug.Log( "OnTriggerExit hit: " + transform.name + " - " + other.transform.name + "\nTags: " + other.tag + "\nMaterial: " + m_mesh.material.name );
			m_canGrip = false;
			m_mesh.material.SetInt( "IsHighlighted", 0 );
		}
	}
}
