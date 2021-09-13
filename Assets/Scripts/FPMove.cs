using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using Debug = UnityEngine.Debug;
public class FPMove : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isGrivate = false;
    void Start()
    {
        
    }


    void Update()
    {
        Debug.Log("fix update");
        if (isGrivate)
        {
            if(Input.GetButtonDown("Jump"))
            {
                Debug.Log("fas");
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    { 
        Debug.Log("碰撞进入");
    }

    private void OnCollisionStay(Collision collision)
    { 
        Debug.Log("碰撞中");
        isGrivate = true;
    }

    private void OnCollisionExit(Collision collision)
    { 
        Debug.Log("碰撞结束");
        isGrivate = false;
    }

    private void OnTriggerEnter(Collider other)
    { Debug.Log("触发进入");
    }

    private void OnTriggerStay(Collider other)
    { Debug.Log("触发中");
    }

    private void OnTriggerExit(Collider other)
    { Debug.Log("触发结束");
    }
}
