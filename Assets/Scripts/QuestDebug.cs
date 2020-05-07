using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDebug : MonoBehaviour
{
    public static QuestDebug Instance;
    private Text m_text;
    private bool m_inMenu;
    public Transform m_rig;

    private void Awake()
    {
        Instance = this;    
    }

    // Start is called before the first frame update
    void Start()
    {
      //  RectTransform labelRect = DebugUIBuilder.instance.AddLabel( "Label" );
      //  m_text = labelRect.GetComponent<Text>();
        
        m_inMenu = false;
    }

    // Update is called once per frame
    void Update()
    {
    //    if ( OVRInput.GetDown( OVRInput.Button.Two ) || OVRInput.GetDown( OVRInput.Button.Start ) )
        {
            if ( m_inMenu )
            {
               // DebugUIBuilder.instance.Hide();
            }
            else
            {
               // DebugUIBuilder.instance.transform.position = m_rig.transform.TransformPoint( new Vector3( 0, 0, 4 ) );
                //DebugUIBuilder.instance.transform.rotation = m_rig.transform.rotation;
               // DebugUIBuilder.instance.Show();
            }
            m_inMenu = !m_inMenu;
        }
    }

    public static void Log( string message )
    {
        //Instance.m_text.text = message;
        Debug.Log("[QuestDebug] " + message );
    }

	public static void Error( string message )
	{
		//Instance.m_text.text = message;
		Debug.Log( "[QuestDebug][ERROR] " + message );
	}
}
