using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class XRHandController : MonoBehaviour
{
	public enum Hand
	{
		Left,
		Right
	}

	public Hand m_handType;
	private Gripper m_gripper;

	private InputDevice m_handDevice;

	// This function is called when the object becomes enabled and active.
	void OnEnable()
	{
		m_gripper = transform.GetComponent<Gripper>();

		// Get all current input devices and register
		List<InputDevice> allCurrentDevices = new List<InputDevice>();
		InputDevices.GetDevices( allCurrentDevices );
		foreach ( var device in allCurrentDevices )
		{
			OnInputDeviceConnected( device );
		}

		// Register callbacks for devices connecting
		InputDevices.deviceConnected += OnInputDeviceConnected;
	}

	// This function is called when the behaviour becomes disabled.
	// This is also called when the object is destroyed and can be used for any cleanup code.When scripts are reloaded after compilation has finished, OnDisable will be called, followed by an OnEnable after the script has been loaded.
	void OnDisable()
	{
		// Unregister callbacks for devices connecting
		InputDevices.deviceConnected -= OnInputDeviceConnected;
	}

	// Update is called once per frame
	void Update()
	{
		if ( m_handDevice == null )
			return;

		Vector3 localPosition = new Vector3();
		Quaternion localRotation = new Quaternion();

		if ( m_handDevice.TryGetFeatureValue( CommonUsages.devicePosition, out localPosition ) )
		{
			transform.localPosition = localPosition;
			//QuestDebug.ConsoleLog( "Changing hand " + transform.name + ": " + localPosition );
		}

		if ( m_handDevice.TryGetFeatureValue( CommonUsages.deviceRotation, out localRotation ) )
		{
			transform.localRotation = localRotation;
		}

		bool gripPressed = false;
		m_handDevice.TryGetFeatureValue( CommonUsages.gripButton, out gripPressed );

		m_gripper.SetGripping( gripPressed );
		
	}

	private void OnInputDeviceConnected( InputDevice obj )
	{
		if ( obj.characteristics.HasFlag( m_handType == Hand.Left ? InputDeviceCharacteristics.Left : InputDeviceCharacteristics.Right ) )
		{
			QuestDebug.ConsoleLog( "Input device connected: " + transform.name );
			m_handDevice = obj;
		}
	}
}
