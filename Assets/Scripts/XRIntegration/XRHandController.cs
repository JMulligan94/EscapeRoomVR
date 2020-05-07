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

	public Hand HandType;

	private InputDevice m_handDevice;

	// Start is called before the first frame update
	void Start()
	{
		List<InputDevice> inputDevices = new List<InputDevice>();
		InputDevices.GetDevicesWithCharacteristics( HandType == Hand.Left ? InputDeviceCharacteristics.Left : InputDeviceCharacteristics.Right, inputDevices );
		m_handDevice = inputDevices.FirstOrDefault();

		if ( m_handDevice != null )
		{
			QuestDebug.Log( "Found input device for hand: " + transform.name );
		}
		else
		{
			QuestDebug.Error( "Couldn't find device for hand: " + transform.name );
		}
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
			QuestDebug.Log( "Changing hand " + transform.name + ": " + localPosition );
		}

		if ( m_handDevice.TryGetFeatureValue( CommonUsages.deviceRotation, out localRotation ) )
		{
			transform.localRotation = localRotation;
		}
	}
}
