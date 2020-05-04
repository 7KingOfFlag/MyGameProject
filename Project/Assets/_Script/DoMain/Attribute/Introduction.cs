namespace OurGameName.DoMain.Attribute
{
    using System;
    using System.Linq;

    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class Introduction : System.Attribute
    {
        public string Msg;

        public Introduction(string msg)
        {
            this.Msg = msg;
        }

        public static string GetMsg(Type objType, AttributeTargets targets, string targetName)
        {
            Object[] attributes = null;
            string msg = null;
            switch (targets)
            {
                case AttributeTargets.Class:
                    try
                    {
                        attributes = objType.GetCustomAttributes(typeof(Introduction), true);
                    }
                    catch { }
                    break;

                case AttributeTargets.Method:
                    try
                    {
                        attributes = objType.GetMethod(targetName).GetCustomAttributes(typeof(Introduction), true);
                    }
                    catch { }
                    break;

                case AttributeTargets.Property:
                    try
                    {
                        attributes = objType.GetProperty(targetName).GetCustomAttributes(typeof(Introduction), true);
                    }
                    catch { }
                    break;

                default:
                    attributes = null;
                    break;
            }

            if (attributes != null)
            {
                Introduction introduction = (Introduction)attributes.FirstOrDefault(att => att is Introduction);
                if (introduction != null)
                {
                    msg = introduction.Msg;
                }
            }
            return msg;
        }
    }
}