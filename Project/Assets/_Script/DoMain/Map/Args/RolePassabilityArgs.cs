namespace OurGameName.DoMain.Map.Service
{
    /// <summary>
    /// 角色通过性参数
    /// </summary>
    public class RolePassabilityArgs
    {
        /// <summary>
        /// 能通过山脉
        /// </summary>
        public bool CanPassMountain { get; set; }

        /// <summary>
        /// 能通过水域
        /// </summary>
        public bool CanPassWater { get; set; }
    }
}