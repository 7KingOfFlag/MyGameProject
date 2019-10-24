using System.Collections.Generic;
using UnityEngine;
using System.Web.Script.Serialization;
using OurGameName.DoMain.Entity;
using UnityEngine.UI;
using System.IO;

namespace OurGameName.Manager
{
    public class PropUI : MonoBehaviour
    {
        public Text ID;
        public Text Name;
        public Text Effect;
        public Text Directory;
        public Text Maker;

        private int PropID;
        public int SelectedID;

        private Prop[] Props;
        private void Awake()
        {
            string path = Application.dataPath + "/Data/Prop/Prop.json";

            PropID = -1;
            SelectedID = 0;
            string jsonStr = null;
            if (true)
            {
                jsonStr = ReadFile(path);
            }

            Props = Prop.JosnDeserialize(jsonStr).ToArray();

        }

        private void Update()
        {
            if (SelectedID != PropID)
            {
                PropID = SelectedID;
                ChangProp(Props[PropID]);
            }
        }

        private void ChangProp(Prop item)
        {
            ID.text = item.ID.ToString();
            Name.text = item.Name;
            Effect.text = item.Effect;
            Directory.text = item.Directory;
            if (item.MakerID == -1)
            {
                Maker.text = "无名氏";
            }
            else
            {

            }
        }

        private void SaveProp(string Josn,string path)
        {
            if (!File.Exists(path))//检查文件是否存在
            {
                using (StreamWriter writer = File.CreateText(path))
                {
                    writer.Write(Josn);
                }
            }
        }
        private string ReadFile(string path)
        {
            if (File.Exists(path))
            {
                using (StreamReader reader = File.OpenText(path))
                {
                    return reader.ReadToEnd();
                }
            }
            else
            {
                return null;
            }
        }
    }

    
}
