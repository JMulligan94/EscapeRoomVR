using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRMainController : MonoBehaviour
{
	public XRDirectInteractor m_leftHandDirect;
	public XRDirectInteractor m_rightHandDirect;

	public XRRayInteractor m_leftHandRay;
	public XRRayInteractor m_rightHandRay;

	private InputDevice m_leftInputDevice;
	private InputDevice m_rightInputDevice;

	private XRInteractorLineVisual m_leftLineVisual;
	private XRInteractorLineVisual m_rightLineVisual;

	void OnEnable()
	{
		InputDevices.deviceConnected += RegisterDevices;
	}

	void OnDisable()
	{
		InputDevices.deviceConnected -= RegisterDevices;
	}

	void RegisterDevices( InputDevice connectedDevice )
	{
		if ( connectedDevice.isValid )
		{
			if ( ( connectedDevice.characteristics & InputDeviceCharacteristics.HeldInHand ) == InputDeviceCharacteristics.HeldInHand )
			{
				if ( ( connectedDevice.characteristics & InputDeviceCharacteristics.Left ) == InputDeviceCharacteristics.Left )
				{
					m_leftInputDevice = connectedDevice;
				}
				else if ( ( connectedDevice.characteristics & InputDeviceCharacteristics.Right ) == InputDeviceCharacteristics.Right )
				{
					m_rightInputDevice = connectedDevice;
				}
			}
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		//m_leftHandRay.enabled = false;
		//m_rightHandRay.enabled = false;

		m_leftLineVisual = m_leftHandRay.GetComponent<XRInteractorLineVisual>();
		m_leftLineVisual.enabled = false;

		m_rightLineVisual = m_rightHandRay.GetComponent<XRInteractorLineVisual>();
		m_rightLineVisual.enabled = false;
	}

	// Update is called once per frame
	void Update()
	{
		UpdateLeftController();
		UpdateRightController();
	}

	private void UpdateLeftController()
	{
		Vector2 leftAxis;
		m_leftInputDevice.TryGetFeatureValue( CommonUsages.primary2DAxis, out leftAxis );
		
		bool prevDown = m_leftLineVisual.enabled;
		bool newDown = leftAxis.y > 0.5f;

		if ( prevDown != newDown )
		{
			m_leftLineVisual.enabled = !m_leftLineVisual.enabled;
			//m_leftHandRay.enabled = !m_leftHandRay.enabled;
		}
	}

	private void UpdateRightController()
	{
		Vector2 rightAxis;
		m_rightInputDevice.TryGetFeatureValue( CommonUsages.primary2DAxis, out rightAxis );

		bool prevDown = m_rightLineVisual.enabled;
		bool newDown = rightAxis.y > 0.5f;

		if ( prevDown != newDown )
		{
			m_rightLineVisual.enabled = !m_rightLineVisual.enabled;
			//m_rightHandRay.enabled = !m_rightHandRay.enabled;
		}
	}
}
