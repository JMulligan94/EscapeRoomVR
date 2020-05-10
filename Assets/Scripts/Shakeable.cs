using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shakeable : MonoBehaviour
{
	public float m_activeThreshold = 0.05f;

	#region Events
	public delegate void ShakeStartEvent( float shakeIntensity );
	public ShakeStartEvent OnShakeStart;

	public delegate void ShakeEndEvent();
	public ShakeEndEvent OnShakeEnd;
	#endregion


	private Grippable m_grippleComp;
	private bool m_isShaking;

	// Start is called before the first frame update
	void Start()
	{
		m_grippleComp = GetComponent<Grippable>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if ( m_grippleComp.IsGripped )
		{
			float shakeIntensity = m_grippleComp.GetDistanceTravelled();
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
		
	}
}
