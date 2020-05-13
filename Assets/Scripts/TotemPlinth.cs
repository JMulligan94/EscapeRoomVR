using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemPlinth : MonoBehaviour
{
	public string m_totemID;

	public GameObject m_connectedTotem;


	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	//void OnCollisionEnter( Collider other )
	//{
	//	QuestDebug.Log( Time.time + " - " + transform.name + " colliding with " + other.transform.name );
	//	if ( other.tag == "Totem" )
	//	{
	//		if ( other.transform.parent.GetComponent<Totem>().m_totemID == m_totemID )
	//		{
	//			// Success!
	//			m_connectedTotem = other.transform.parent.gameObject;
	//			m_connectedTotem.GetComponent<Totem>().AttachToPlinth();
	//		}
	//	}
	//}

	//void OnCollisionExit( Collisio other )
	//{
	//	if ( m_connectedTotem != null 
	//		&& other.transform.parent.gameObject == m_connectedTotem )
	//	{
	//		m_connectedTotem = null;
	//		m_connectedTotem.GetComponent<Totem>().DetachFromPlinth();
	//	}
	//}
}
