using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRExclusiveSocketInteractor : XRSocketInteractor
{
	public string m_acceptedType = null;

	public override bool CanHover( XRBaseInteractable interactable )
	{
		return CanSelect( interactable );
	}

	public override bool CanSelect( XRBaseInteractable interactable )
	{
		QuestDebug.Log( "CanSelect with: " + transform.name + " and " + interactable.transform.name );
		XRSocketInteractable socketInteractable = interactable.GetComponent<XRSocketInteractable>();
		if ( socketInteractable == null )
			return false;

		return base.CanSelect( interactable ) && socketInteractable.m_socketType == m_acceptedType ;
	}
}
