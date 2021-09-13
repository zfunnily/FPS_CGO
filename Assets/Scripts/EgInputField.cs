using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EgInputField : MonoBehaviour
{
    public InputField Account; //账号输入框
    public InputField Password; //密码框
    public Button LoginButton;

    // Start is called before the first frame update
    void Start()
    {
        Account = GameObject.Find("Account").GetComponent<InputField>();
        Password = GameObject.Find("Password").GetComponent<InputField>();
        LoginButton.onClick.AddListener(Login);
    }
    public void Login()
    {
        UnityEngine.Debug.Log("1231231");
        print("账号: " + Account.text + "; 密码: " + Account.text);
    }
    // Update is called once per frame
}
