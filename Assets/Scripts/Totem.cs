using System;
using System.Timers;
using UnityEngine;

public class Totem : MonoBehaviour
{
	public string m_totemID;
	public Material m_originalMaterial;
	public Material m_correctMaterial;

	private Shakeable m_shakeable;
	private AudioSource m_totemSFX;

	private bool m_hasStopped = false;

	private bool m_isShaking = false;
	private Timer m_shakingTimer;
	private const int m_timerInterval = 1000;
	
	void OnEnable()
	{
		m_shakingTimer = new Timer( m_timerInterval );
		m_shakingTimer.Elapsed += OnShakingTimerElapsed;
		m_shakingTimer.AutoReset = true;

		m_shakeable = GetComponent<Shakeable>();
		m_totemSFX = GetComponent<AudioSource>();
		m_totemSFX.Play();
		m_totemSFX.Pause();

		m_shakeable.OnShakeStart += OnShakeStart;
		m_shakeable.OnShakeEnd += OnShakeEnd;

		m_originalMaterial = GetComponent<MeshRenderer>().material;

		m_isShaking = false;
	}

	// Called if script was disabled during frame 
	void OnDisable()
	{
		m_shakeable.OnShakeStart -= OnShakeStart;
		m_shakeable.OnShakeEnd -= OnShakeEnd;
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void OnShakeStart( float shakeIntensity )
	{
		if ( !m_isShaking )
		{
			m_shakingTimer.Start();
			m_totemSFX.UnPause();
		}

		m_hasStopped = false;

		// Range 
		//float volume = Math.Min( shakeIntensity * 2.0f, 1.0f );
		//QuestDebug.Log( Time.time + ":\nShake volume: " + transform.name + "\n" + volume + " (" + shakeIntensity + ")" );
		//m_totemSFX.volume = volume;
		m_isShaking = true;
	}

	public void OnShakeEnd()
	{
		m_hasStopped = true;
		m_isShaking = false;
		TryStopShaking();
	}

	public void AttachToPlinth()
	{
		GetComponent<MeshRenderer>().material = m_correctMaterial;
	}

	public void DetachFromPlinth()
	{
		GetComponent<MeshRenderer>().material = m_originalMaterial;
	}

	private void OnShakingTimerElapsed( object sender, ElapsedEventArgs e )
	{
		m_shakingTimer.Enabled = false;
		TryStopShaking();
	}

	private void TryStopShaking()
	{
		if ( m_hasStopped && !m_shakingTimer.Enabled)
		{
			m_hasStopped = false;
			m_totemSFX.Pause();
		}
	}
}
