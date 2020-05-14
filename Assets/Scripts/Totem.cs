using System;
using System.Timers;
using UnityEngine;

public class Totem : XRSocketInteractable
{
	public Material m_originalMaterial;
	public Material m_correctMaterial;

	private Shakeable m_shakeable;
	private AudioSource m_totemSFX;

	private float m_sfxVolume = 0.0f;
	
	private const int m_timerInterval = 1000;
	
	void OnEnable()
	{
		m_shakeable = GetComponent<Shakeable>();
		m_totemSFX = GetComponent<AudioSource>();
		m_totemSFX.Play();
		m_totemSFX.Pause();

		m_shakeable.OnShakeStart += OnShakeStart;
		m_shakeable.OnShakeEnd += OnShakeEnd;

		m_originalMaterial = GetComponent<MeshRenderer>().material;
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
		float volumeDrop = Time.deltaTime * 0.1f; // 0.1 per sec
		m_sfxVolume = Math.Max(m_sfxVolume - volumeDrop, 0.0f);
		m_totemSFX.volume = m_sfxVolume;

		if ( m_sfxVolume == 0.0f )
			m_totemSFX.Pause();
	}

	public void OnShakeStart( float shakeIntensity )
	{
		if ( !m_totemSFX.isPlaying )
			m_totemSFX.UnPause();

		m_sfxVolume += Time.deltaTime * 0.2f;
		m_totemSFX.volume = m_sfxVolume;
	}

	public void OnShakeEnd()
	{
	}

	public void AttachToPlinth()
	{
		GetComponent<MeshRenderer>().material = m_correctMaterial;
	}

	public void DetachFromPlinth()
	{
		GetComponent<MeshRenderer>().material = m_originalMaterial;
	}
}
