using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using OurGameName.DoMain.Entity.RoleSpace;


namespace OurGameName.Manager
{
    public class RoleManager : MonoBehaviour {
        List<Role> RoleList;
	    // Use this for initialization
	    void Start () {
            RoleList = new List<Role>();
            Inti();
        }
	
	    // Update is called once per frame
	    void Update () {
		
	    }

        public Role GetRole(int RoleID)
        {
            return RoleList.FirstOrDefault(role => role.ID == RoleID);
        }
        
        private void Inti()
        {
            Role role1 = new Role("响", "当当", "1998.01.01", 10, 20, 30, 40, 50, 0, "无势力", "凡人");
            RoleList.Add(role1);

            Role role2 = new Role("漆", "貂蝉", "2000.04.01", 50, 33, 21, 55, 23, 500, "灵山派", "炼体期", "/image/png1.jpg");
            RoleList.Add(role2);
        }

    }

}
