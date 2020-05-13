using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetInteractable : XRGrabInteractable
{
	private Vector3 m_savedPosition;
	private Quaternion m_savedRotation;
	private	Rigidbody m_rigidbody;
	public MeshRenderer m_mesh;

	protected override void Awake()
	{
		base.Awake();
		m_rigidbody = GetComponent<Rigidbody>();
	}

	protected override void OnHoverEnter( XRBaseInteractor interactor )
	{
		//SetSelectionHighlight( true );
		base.OnHoverEnter( interactor );
	}

	protected override void OnHoverExit( XRBaseInteractor interactor )
	{
		//SetSelectionHighlight( false );
		base.OnHoverExit( interactor );
	}

	protected override void OnSelectEnter( XRBaseInteractor interactor )
	{
		m_savedPosition = interactor.attachTransform.localPosition;
		m_savedRotation = interactor.attachTransform.localRotation;

		interactor.attachTransform.position = attachTransform != null ? attachTransform.position : m_rigidbody.worldCenterOfMass;
		interactor.attachTransform.rotation = attachTransform != null ? attachTransform.rotation : m_rigidbody.rotation;

		base.OnSelectEnter( interactor );
	}

	protected override void OnSelectExit( XRBaseInteractor interactor )
	{
		interactor.attachTransform.localPosition = m_savedPosition;
		interactor.attachTransform.localRotation = m_savedRotation;

		base.OnSelectExit( interactor );
	}

	public void SetSelectionHighlight( bool highlight )
	{
		m_mesh.material.SetInt( "IsHighlighted", highlight ? 1 : 0 );
	}
}
