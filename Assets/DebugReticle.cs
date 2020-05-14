using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugReticle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		QuestDebug.Log( transform.name + " pos: " + transform.position );
    }
}
