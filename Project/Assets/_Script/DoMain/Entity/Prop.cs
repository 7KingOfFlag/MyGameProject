using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace OurGameName.DoMain.Entity
{
    public class Prop
    {
        private static int count = 0;

        public int ID { get; set; }
        public int ParentID { get; set; }
        public string Name { get; set; }
        public string Effect { get; set; }
        public string Directory { get; set; }
        public int MakerID { get; set; }

        public Prop()
        {
        }

        public Prop(int parentID, string name, string effect, string directory, int makerID)
        {
            ID = count++;
            ParentID = parentID;
            Name = name;
            Effect = effect;
            Directory = directory;
            MakerID = makerID;
        }

        public static List<Prop> JosnDeserialize(string JosnStr)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<List<Prop>>(JosnStr);
        }
    }
}