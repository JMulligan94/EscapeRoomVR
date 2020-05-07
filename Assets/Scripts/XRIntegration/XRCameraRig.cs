using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class XRCameraRig : MonoBehaviour
{
	private InputDevice m_hmdDevice;

	// Start is called before the first frame update
	void Start()
	{
		// Get HMD device
		List<InputDevice> inputDevices = new List<InputDevice>();
		InputDevices.GetDevicesWithCharacteristics( InputDeviceCharacteristics.HeadMounted, inputDevices );
		m_hmdDevice = inputDevices.FirstOrDefault();

		if ( m_hmdDevice != null )
		{
			QuestDebug.Log( "Found HMD device: " + transform.name + " " + m_hmdDevice.name + ", " + m_hmdDevice.manufacturer );
		}
		else
		{
			QuestDebug.Error( "Couldn't find HMD device" );
		}
	}

	// Update is called once per frame
	void Update()
	{
		if ( m_hmdDevice == null )
			return;

		Vector3 localPosition = new Vector3();
		Quaternion localRotation = new Quaternion();

		if ( m_hmdDevice.TryGetFeatureValue( CommonUsages.devicePosition, out localPosition ) )
		{
			transform.localPosition = localPosition;
		}

		if ( m_hmdDevice.TryGetFeatureValue( CommonUsages.deviceRotation, out localRotation ) )
		{
			transform.localRotation = localRotation;
		}
	}
}
