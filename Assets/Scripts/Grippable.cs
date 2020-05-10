using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grippable : MonoBehaviour
{
	public MeshRenderer m_mesh;
	public Rigidbody m_rigidBody;
	public bool IsGripped
	{
		get;
		private set;
	} = false;

	private Transform m_originalParent;

	// Transform history
	private Vector3[] m_grippableLastPositions;
	private int m_currentFrameIndex = 0;
	private const int c_timeSteps = 5;


	// Start is called before the first frame update
	void Start()
	{
		m_originalParent = transform.parent;

		ResetVelocities();
	}

	// Frame-rate independent message for physics calculations.
	void FixedUpdate()
	{
		if ( IsGripped )
		{
			m_grippableLastPositions[ m_currentFrameIndex % c_timeSteps ] = transform.position;
			m_currentFrameIndex++;
		}
	}

	public void OnGrip(Transform anchorPoint)
	{
		SetHighlighted( false );
		transform.SetParent( anchorPoint );
		m_rigidBody.isKinematic = true;
		m_rigidBody.useGravity = false;
		IsGripped = true;
	}

	public void OnRelease()
	{
		transform.SetParent( m_originalParent );
		m_rigidBody.isKinematic = false;
		m_rigidBody.useGravity = true;
		IsGripped = false;

		ResetVelocities();
	}

	public void OnTriggerEnter( Collider other )
	{
		if ( IsGripped )
			return;

		if ( other.tag == "Hand" )
		{
			SetHighlighted( true );
			other.GetComponent<Gripper>().AddGrippable( this );
		}
	}

	public void OnTriggerExit( Collider other )
	{
		if ( IsGripped )
			return;

		if ( other.tag == "Hand" )
		{
			SetHighlighted( false );
			other.GetComponent<Gripper>().RemoveGrippable( this );
		}
	}


	public float GetDistanceTravelled()
	{
		float magnitude = 0.0f;
		if ( m_currentFrameIndex < c_timeSteps )
			return magnitude;

		for ( int i = c_timeSteps - 1; i >= 1; --i )
		{
			magnitude += ( m_grippableLastPositions[ i ] - m_grippableLastPositions[i-1] ).magnitude;
		}
		return magnitude;
	}

	internal void ApplyForce(Vector3 forceVector)
	{
		m_rigidBody?.AddForce( forceVector, ForceMode.Impulse );
	}

	internal Vector3 GetAverageVelocity()
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

	private void SetHighlighted( bool highlighted )
	{
		m_mesh.material.SetInt( "IsHighlighted", highlighted ? 1 : 0 );
	}

	private void ResetVelocities()
	{
		m_grippableLastPositions = new Vector3[ c_timeSteps ];
		m_currentFrameIndex = 0;
	}

}
