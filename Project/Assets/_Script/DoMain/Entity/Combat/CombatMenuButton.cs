using UnityEngine;
using UnityEngine.UI;

namespace OurGameName.DoMain.Entity.Combat
{
    /// <summary>
    /// 战斗菜单按钮
    /// </summary>
    public class CombatMenuButton : MonoBehaviour
    {
        public Button btn;
        private Text txt;
        private Image backgound;
        private Skill skill;

        /// <summary>
        /// 子菜单
        /// </summary>
        public CombatMenu SonMenu;

        private void Awake()
        {
            btn = GetComponent<Button>();
            txt = GetComponentInChildren<Text>();
            backgound = GetComponent<Image>();
        }

        public Skill Skill
        {
            get
            {
                return skill;
            }

            set
            {
                skill = value;
                txt.text = skill.Name;
            }
        }

        public bool Enabled
        {
            get
            {
                return enabled;
            }

            set
            {
                enabled = value;
                btn.enabled = enabled;
                backgound.enabled = enabled;
                txt.enabled = enabled;
            }
        }
    }
}