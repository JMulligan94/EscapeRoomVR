using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent( typeof( XROffsetInteractable ) )]
public class Shakeable : MonoBehaviour
{
	public float m_activeThreshold = 0.05f;

	#region Events
	public delegate void ShakeStartEvent( float shakeIntensity );
	public ShakeStartEvent OnShakeStart;

	public delegate void ShakeEndEvent();
	public ShakeEndEvent OnShakeEnd;
	#endregion
	
	private bool m_isShaking;
	private XROffsetInteractable m_interactable;

	// Transform history
	private Vector3[] m_lastPositions;
	private int m_currentFrameIndex = 0;
	private const int c_timeSteps = 5;

	// Start is called before the first frame update
	void Start()
	{
		m_interactable = GetComponent<XROffsetInteractable>();
		ResetVelocities();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if ( m_interactable.isSelected )
		{
			m_lastPositions[ m_currentFrameIndex % c_timeSteps ] = transform.position;
			m_currentFrameIndex++;

			float shakeIntensity = GetDistanceTravelled();
			//QuestDebug.ConsoleLog( "IsGripped - Intensity = " + shakeIntensity );
			if ( shakeIntensity >= m_activeThreshold )
			{
				OnShakeStart?.Invoke( shakeIntensity );
				m_isShaking = true;
			}
			else if ( m_isShaking )
			{
				//QuestDebug.Log( Time.time + ":\nShake stopped" );
				OnShakeEnd?.Invoke();
				m_isShaking = false;
			}
		}
		else if ( m_currentFrameIndex  > 0 )
		{
			ResetVelocities();
		}
	}

	private float GetDistanceTravelled()
	{
		float magnitude = 0.0f;
		if ( m_currentFrameIndex < c_timeSteps )
			return magnitude;

		for ( int i = c_timeSteps - 1; i >= 1; --i )
		{
			magnitude += ( m_lastPositions[ i ] - m_lastPositions[ i - 1 ] ).magnitude;
		}
		return magnitude;
	}

	private void ResetVelocities()
	{
		m_lastPositions = new Vector3[ c_timeSteps ];
		m_currentFrameIndex = 0;
	}
}
