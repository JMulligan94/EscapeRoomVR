using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gripper : MonoBehaviour
{
	public Transform m_anchorPoint;

	public bool IsGripping = false;

	private List<Grippable> m_grippables;

	private Grippable m_currentGrippable;

	private Collider m_collider;

	// Start is called before the first frame update
	void Start()
	{
		m_grippables = new List<Grippable>();
		m_collider = GetComponent<Collider>();
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	internal void AddGrippable( Grippable grippable )
	{
		m_grippables.Add( grippable );
	}

	internal void RemoveGrippable( Grippable grippable )
	{
		if ( m_grippables.Contains( grippable ) )
			m_grippables.Remove( grippable );
	}

	public void SetGripping( bool gripPressed )
	{
		if ( IsGripping != gripPressed )
		{
			IsGripping = gripPressed;

			QuestDebug.Log( "Grip pressed changed: " + gripPressed );
			if ( gripPressed )
				OnGrip();
			else
				OnRelease();
		}
	}

	private void OnGrip()
	{
		if ( m_grippables.Count == 0 )
			return;

		Grippable objectToGrip = m_grippables.FirstOrDefault();
		m_currentGrippable = objectToGrip;
		objectToGrip.OnGrip( m_anchorPoint );
	}

	private void OnRelease()
	{
		if ( m_currentGrippable == null )
			return;
		
		m_currentGrippable.OnRelease();
		RemoveGrippable( m_currentGrippable );
		m_currentGrippable = null;
	}
}
