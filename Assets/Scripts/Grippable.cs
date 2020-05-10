using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grippable : MonoBehaviour
{
	public MeshRenderer m_mesh;
	public Rigidbody m_rigidBody;

	private Transform m_originalParent;

	private bool m_gripped = false;

	// Start is called before the first frame update
	void Start()
	{
		m_originalParent = transform.parent;
	}

	// Update is called once per frame
	void Update()
	{
	}

	public void OnGrip(Transform anchorPoint)
	{
		SetHighlighted( false );
		transform.SetParent( anchorPoint );
		m_rigidBody.isKinematic = true;
		m_rigidBody.useGravity = false;
		m_gripped = true;
	}

	public void OnRelease()
	{
		transform.SetParent( m_originalParent );
		m_rigidBody.isKinematic = false;
		m_rigidBody.useGravity = true;
		m_gripped = false;
	}

	public void OnTriggerEnter( Collider other )
	{
		if ( m_gripped )
			return;

		if ( other.tag == "Hand" )
		{
			SetHighlighted( true );
			other.GetComponent<Gripper>().AddGrippable( this );
		}
	}

	public void OnTriggerExit( Collider other )
	{
		if ( m_gripped )
			return;

		if ( other.tag == "Hand" )
		{
			SetHighlighted( false );
			other.GetComponent<Gripper>().RemoveGrippable( this );
		}
	}

	private void SetHighlighted( bool highlighted )
	{
		m_mesh.material.SetInt( "IsHighlighted", highlighted ? 1 : 0 );
	}

	internal Rigidbody GetRigidBody()
	{
		return m_rigidBody;
	}
}
