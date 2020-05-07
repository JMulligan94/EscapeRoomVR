using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class XRInputManager : MonoBehaviour
{
	public class XRInputState
	{
		public bool GripPressed = false;

		public void ResetState()
		{
			GripPressed = false;
		}
	}

	private XRInputState m_prevState;
	private XRInputState m_currentState;
	private List<InputDevice> m_inputDevices;

	// This function is called when the object becomes enabled and active.
	void OnEnable()
	{
		m_inputDevices = new List<InputDevice>();
		m_currentState = new XRInputState();
		m_prevState = new XRInputState();

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
		m_prevState = m_currentState;

		bool gripPressed = false;
		foreach ( InputDevice inputDevice in m_inputDevices )
		{
			bool buttonPressedState = false;
			if ( inputDevice.TryGetFeatureValue( CommonUsages.gripButton, out buttonPressedState ) )
				gripPressed |= buttonPressedState; 
		}

		m_currentState.GripPressed = gripPressed;

		if ( m_prevState.GripPressed != m_currentState.GripPressed )
		{
			if ( m_currentState.GripPressed )
				Grippable.GripButtonPressed();
			else
				Grippable.GripButtonReleased();
		}
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
