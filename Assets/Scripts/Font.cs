using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Font : MonoBehaviour
{
    public Text TestText;
    // Start is called before the first frame update
    void Start()
    {
        TestText.fontSize = 15;
        TestText.fontStyle = FontStyle.Normal;
        TestText.color = UnityEngine.Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
