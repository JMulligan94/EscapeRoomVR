using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRReleaseController : XRController
{
	private bool m_selected = false;
	private bool m_isActive = false;

	private FieldInfo m_activatedThisFrameInfo;
	private FieldInfo m_deactivatedThisFrameInfo;
	private FieldInfo m_activeInfo;
	private FieldInfo m_selectionStateInfo;

	protected override void OnEnable()
	{
		base.OnEnable();

		var interactionType = typeof( XRController ).Assembly.GetType( "UnityEngine.XR.Interaction.Toolkit.XRController+InteractionState" );
		m_activatedThisFrameInfo = interactionType.GetField( "activatedThisFrame" );
		m_deactivatedThisFrameInfo = interactionType.GetField( "deActivatedThisFrame" );
		m_activeInfo = interactionType.GetField( "active" );

		m_selectionStateInfo = typeof( XRController ).GetField( "m_SelectInteractionState", BindingFlags.Instance | BindingFlags.NonPublic );
	}

	private void LateUpdate()
	{
		object selectionState = m_selectionStateInfo.GetValue( this );
		if ( m_selected )
		{
			if ( !m_isActive )
			{
				m_activatedThisFrameInfo.SetValue( selectionState, true );
				m_activeInfo.SetValue( selectionState, true );
				m_isActive = true;

				m_selectionStateInfo.SetValue( this, selectionState );
			}
		}
		else
		{
			if ( !m_isActive )
			{
				m_deactivatedThisFrameInfo.SetValue( selectionState, true );
				m_activeInfo.SetValue( selectionState, false );
				m_isActive = false;

				m_selectionStateInfo.SetValue( this, selectionState );
			}
		}
		m_selected = false;
	}

	public void Select()
	{
		m_selected = true;
	}
}
