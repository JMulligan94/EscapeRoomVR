using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class XRInputManager : MonoBehaviour
{
	private List<InputDevice> m_inputDevices;

	// This function is called when the object becomes enabled and active.
	void OnEnable()
	{
		m_inputDevices = new List<InputDevice>();
		// Get all current input devices and register
		List<InputDevice> allCurrentDevices = new List<InputDevice>();
		InputDevices.GetDevices( allCurrentDevices );
		foreach ( var device in allCurrentDevices )
		{
			OnInputDeviceConnected( device );
		}

		// Register callbacks for devices connecting/disconnecting
		InputDevices.deviceConnected += OnInputDeviceConnected;
		InputDevices.deviceDisconnected += OnInputDevicesDisconnected;
	}

	// This function is called when the behaviour becomes disabled.
	// This is also called when the object is destroyed and can be used for any cleanup code.When scripts are reloaded after compilation has finished, OnDisable will be called, followed by an OnEnable after the script has been loaded.
	void OnDisable()
	{
		// Unregister callbacks for devices connecting/disconnecting
		InputDevices.deviceConnected -= OnInputDeviceConnected;
		InputDevices.deviceDisconnected -= OnInputDevicesDisconnected;
	}
	
	// Update is called once per frame
	void Update()
	{
		bool buttonDownState = false;
		foreach ( InputDevice inputDevice in m_inputDevices )
		{
			bool buttonPressedState = false;
			if ( inputDevice.TryGetFeatureValue( CommonUsages.primaryButton, out buttonPressedState ) )
				buttonDownState |= buttonPressedState; 
		}

		if ( buttonDownState )
			QuestDebug.Log( "A button has been pressed!" );
	
	}

	private void OnInputDeviceConnected( InputDevice obj )
	{
		m_inputDevices.Add( obj );
	}

	private void OnInputDevicesDisconnected( InputDevice obj )
	{
		if (m_inputDevices.Contains(obj))
			m_inputDevices.Remove( obj );
	}
}
