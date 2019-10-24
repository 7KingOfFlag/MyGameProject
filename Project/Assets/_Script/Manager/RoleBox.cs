using OurGameName.DoMain;
using OurGameName.DoMain.Attribute;
using OurGameName.DoMain.Entity;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using OurGameName.DoMain.Entity.RoleSpace;


namespace OurGameName.Manager
{
    public class RoleBox : MonoBehaviour
    {
        public int roleID;
        private int lastRoleID;
        private RoleManager roleManager;

        private GameObject txtAttribute;   //详情提示文本框
        private GameObject thisTxtAttribute;
        private bool isHover = false;
        private Text[] txts;
        private PropertyInfo[] roleProperties;
        RawImage Picture;
        // Use this for initialization
        void Start()
        {
            roleManager = GameObject.Find("RoleManager").GetComponent<RoleManager>();

            Role role = roleManager.GetRole(0);
            roleProperties = role.GetType().GetProperties();
            txts = new Text[roleProperties.Length];

            Transform childCanvas = transform.Find("property");

            for (int i = 0; i < roleProperties.Length; i++)
            {
                Transform t = transform.Find(roleProperties[i].Name);
                if (t == null)
                {
                    t = childCanvas.Find(roleProperties[i].Name);
                }
                if (t != null)
                {
                    txts[i] = t.Find("txt").GetComponent<Text>();
                    EventTriggerListener.Get(t.gameObject).onEnter = OnEnter;
                    EventTriggerListener.Get(t.gameObject).onExit = OnExit;
                }
            }

            transform.Find("Picture").GetComponent<RawImage>();
            roleID = 0;
            lastRoleID = -1;

            txtAttribute = GameObject.Find("txtAttribute");
            Picture = transform.Find("Picture").GetComponent<RawImage>();
        }

        // Update is called once per frame
        void Update()
        {
            if (roleID != lastRoleID)
            {
                Refresh(roleID);
                lastRoleID = roleID;
            }


        }

        private void Refresh(int roleID)
        {
            Role role = roleManager.GetRole(roleID);
            if (role != null)
            {
                for (int i = 0; i < txts.Length; i++)
                {
                    if (txts[i] != null)
                    {
                        string name = roleProperties[i].Name;

                        switch (name)
                        {
                            case "Forces":
                            case "Status":
                                txts[i].text = role.GetType().GetProperty(name).GetValue(role, null).ToString();
                                break;
                            case "Name":
                                string FamilyName, Name;
                                FamilyName = role.GetType().GetProperty("FamilyName").GetValue(role, null).ToString();
                                Name = role.GetType().GetProperty("Name").GetValue(role, null).ToString();
                                txts[i].text = string.Format("{0} {1}", FamilyName, Name);
                                break;
                            default:
                                string str = role.GetType().GetProperty(name).GetValue(role, null).ToString();
                                string Msg = Introduction.getMsg(role.GetType(), System.AttributeTargets.Property, name);

                                txts[i].text = string.Format("{0} ：{1} ", Msg, str);
                                break;
                        }
                    }

                }
                if (role.PicturePath != null)
                {
                    try
                    {
                        var path = Application.dataPath + role.PicturePath;

                        Picture.texture = FileHelper.LoadTexture2D(path,270,180);
                    }
                    catch (FileNotFoundException)
                    {
                        userDefault();
                    }
                }
                else
                {
                    userDefault();
                }
            }
            else
            {
                Debug.LogError("role == null");
            }

        }

        private void userDefault()
        {
            var path = Application.dataPath + "/image/default.jpg";
            Picture.texture = FileHelper.LoadTexture2D(path, 270, 180);
        }

        

        

        private void OnEnter(GameObject g)
        {
            isHover = true;
            if (isHover == true && thisTxtAttribute == null)
            {
                GameObject mainUI = GameObject.Find("MainUI");
                thisTxtAttribute = Instantiate(txtAttribute, Input.mousePosition, new Quaternion(0, 0, 0, 0), mainUI.transform);
                thisTxtAttribute.transform.Find("txt").GetComponent<Text>().text =
                    Introduction.getMsg(typeof(Role), System.AttributeTargets.Property, g.name);
            }

        }

        private void OnExit(GameObject g)
        {
            isHover = false;
            Destroy(thisTxtAttribute);
        }

    }
}