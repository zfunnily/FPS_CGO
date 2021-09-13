using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : MonoBehaviour
{
    public bool ListenInFunction()
    {
        print("调用");
        return true;
    }
    // Start is called before the first frame update
    void Start()
    {
        // GameObject.Find("Toggle").GetComponent<Toggle>().OnChangeValue.AddListener(isOpen => print("调用"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
