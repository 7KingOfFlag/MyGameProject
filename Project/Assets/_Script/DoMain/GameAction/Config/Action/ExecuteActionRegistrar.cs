namespace OurGameName.DoMain.GameAction.Config.Action
{
    using OurGameName.DoMain.GameAction.Action;
    using OurGameName.DoMain.GameAction.Config.Action.ExecuteAction;

    public sealed class ExecuteActionRegistrar
    {
        public ExecuteActionRegistrar()
        {
            var container = ActionContainer.Instance;
            container.Registered<DamageAction>();
            container.Registered<HealingAction>();
        }
    }
}