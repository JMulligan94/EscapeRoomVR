using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grippable : MonoBehaviour
{
	public MeshRenderer m_mesh;
	public Rigidbody m_rigidBody;

	private Transform m_originalParent;

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
		transform.SetParent( anchorPoint );
		m_rigidBody.isKinematic = true;
		m_rigidBody.useGravity = false;
	}

	public void OnRelease()
	{
		transform.SetParent( m_originalParent );
		m_rigidBody.isKinematic = false;
		m_rigidBody.useGravity = true;
	}

	public void OnTriggerEnter( Collider other )
	{
		if ( other.tag == "Hand" )
		{
			m_mesh.material.SetInt( "IsHighlighted", 1 );
			other.GetComponent<Gripper>().AddGrippable( this );
		}
	}

	public void OnTriggerExit( Collider other )
	{
		if ( other.tag == "Hand" )
		{
			m_mesh.material.SetInt( "IsHighlighted", 0 );
			other.GetComponent<Gripper>().RemoveGrippable( this );
		}
	}
}
