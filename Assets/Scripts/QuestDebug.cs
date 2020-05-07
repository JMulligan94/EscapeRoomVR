using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDebug : MonoBehaviour
{
    public static QuestDebug Instance;
    public Text m_text;
	public static Color m_defaultColour;
	public static Color m_errorColour;

    private void Awake()
    {
        Instance = this;    
    }

    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
    }

	public static void Log( string message )
	{
		if ( Instance.m_text != null )
		{
			Instance.m_text.text = message;
			Instance.m_text.color = m_defaultColour;
		}
		ConsoleLog( message );
	}

	public static void Error( string message )
	{
		if ( Instance.m_text != null )
		{
			Instance.m_text.text = message;
			Instance.m_text.color = m_errorColour;
		}
		ConsoleError( message );
	}

	public static void ConsoleLog( string message )
    {
        //Instance.m_text.text = message;
        Debug.Log("[QuestDebug] " + message );
    }

	public static void ConsoleError( string message )
	{
		//Instance.m_text.text = message;
		Debug.Log( "[QuestDebug][ERROR] " + message );
	}
}
