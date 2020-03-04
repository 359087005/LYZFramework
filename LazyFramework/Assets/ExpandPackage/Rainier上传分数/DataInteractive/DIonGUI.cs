using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DIonGUI : MonoBehaviour
{
    [Header("显示时间")]
    public float time = 9;
    public GUISkin mySkin;

    private bool JIsClick = false;
    private float clickNum = 0;
    private bool isDisplayGUI = false;
    private float timer = 0;
    private bool isStartCount = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            JIsClick = true;
        }
        if (JIsClick)
        {
            clickNum += Time.deltaTime;
            if (clickNum >3)
            {
                JIsClick = false;
                clickNum = 0;
                return;
            }
            if (Input.GetKey(KeyCode.A))
            {
                isDisplayGUI = true;
                JIsClick = false;
                clickNum = 0;               
            }
        }
        if (isStartCount)
        {
            timer += Time.deltaTime;
            if (timer >= time)
            {
                timer = 0;
                isStartCount = false;
                isDisplayGUI = false;
            }
        }
        else
        {
            timer = 0;
        }
    }
    private Vector2 scrollViewVector = Vector2.zero;
    private string textFieldString = "";
    private bool isAlwaysDisplay = false;

    private void OnGUI()
    {
        GUI.skin = mySkin;
        if (isDisplayGUI)
        {
            // Begin the ScrollView

            scrollViewVector = GUI.BeginScrollView(new Rect(25, 25,Screen.width / 2 - 75, Screen.height/2), scrollViewVector, new Rect(25,25,Screen.width / 2 - 75,Screen.height),false,false);          
            // Put something inside the ScrollView
            // GUI.TextArea(new Rect(25,25, Screen.width / 2 - 75, Screen.height  ), inputField.text);
            GUI.TextArea(new Rect(25,25, Screen.width / 2 - 75, Screen.height), ExpInteractive.Instance.GetInfo());
            // End the ScrollView
            GUI.EndScrollView();
            textFieldString = GUI.TextField(new Rect(Screen.width / 2 - 50, Screen.height / 2-15, 100, 30), textFieldString);
            isAlwaysDisplay = GUI.Toggle(new Rect(Screen.width / 2 -45, Screen.height / 2-45, 100, 30), isAlwaysDisplay, "总是显示");
            if(!isAlwaysDisplay) 
            {
                isStartCount=true;              
            }                
            else isStartCount=false;
            if (GUI.Button(new Rect(Screen.width / 2 - 25, Screen.height / 2 +30, 50, 30), "确定"))
            {
                ExpScore.Instance.SendScore(int.Parse(textFieldString));
                if(isAlwaysDisplay) return;
                isDisplayGUI = false;
            }

        }
    }
}
