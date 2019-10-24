using UnityEngine;
using UnityEngine.UI;
using OurGameName.DoMain;

namespace OurGameName.Manager
{
    public class UIMain1 : MonoBehaviour
    {
        Button btn1;
        Button btn2;
        RoleBox roleBox;
        public Canvas MainUI;
        void Start()
        {
            btn1 = transform.Find("btn1").GetComponent<Button>();
            btn2 = transform.Find("btn2").GetComponent<Button>();
            EventTriggerListener.Get(btn1.gameObject).onClick = OnButtonClick;
            EventTriggerListener.Get(btn2.gameObject).onClick = OnButtonClick;

            roleBox = transform.Find("RoleBox").GetComponent<RoleBox>();

        }
 
        private void OnButtonClick(GameObject g)
        {
            if (g == btn1.gameObject)
            {
                roleBox.roleID = 0;
            }

            if (g == btn2.gameObject)
            {
                roleBox.roleID = 1;
            }

        }
    }
}
