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
	private Vector3[] m_grippableLastPositions;
	private int m_currentFrameIndex = 0;

	private Collider m_collider;

	private const int c_timeSteps = 5;

	// Start is called before the first frame update
	void Start()
	{
		m_grippables = new List<Grippable>();
		m_collider = GetComponent<Collider>();

		ResetVelocities();
	}

	// Frame-rate independent message for physics calculations.
	void FixedUpdate()
	{
		if ( m_currentGrippable != null )
		{
			m_grippableLastPositions[m_currentFrameIndex % c_timeSteps] = m_currentGrippable.transform.position;
			m_currentFrameIndex++;
		}
	}

	private void ResetVelocities()
	{
		m_grippableLastPositions = new Vector3[ c_timeSteps ];
		m_currentFrameIndex = 0;
	}

	private Vector3 GetAverageVelocity()
	{
		Vector3 sumVector = Vector3.zero;
		int numVectors = 0;
		for ( int i = 0; i < c_timeSteps; ++i )
		{
			if ( m_grippableLastPositions[ i ] != null )
			{
				sumVector += m_grippableLastPositions[ i ];
				numVectors++;
			}
		}

		return sumVector / numVectors;
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

		Vector3 averageVel = GetAverageVelocity();
		Vector3 throwForce = m_currentGrippable.transform.position - averageVel;
		QuestDebug.Log( "Applying throw vector of: " + ( throwForce * 10 ) );
		QuestDebug.Log( "Last transform: " + averageVel
			+ "\n This transform: " + m_currentGrippable.transform.position
			+ "\nApplying throw vector of: " + (throwForce * 10) );
		m_currentGrippable.GetRigidBody().AddForce( throwForce * 10, ForceMode.Impulse );

		m_currentGrippable = null;
		ResetVelocities();
	}
}
