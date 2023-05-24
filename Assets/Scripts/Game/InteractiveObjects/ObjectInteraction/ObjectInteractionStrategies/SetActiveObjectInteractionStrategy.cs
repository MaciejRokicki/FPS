public class SetActiveObjectInteractionStrategy : BaseObjectInteractionStrategy
{
    public SetActiveObjectInteractionStrategy(ObjectInteraction objectInteraction) : base(objectInteraction)
    {
    }

    public override void Interact()
    {
        objectInteraction.InteractionObjectData.GameObject.SetActive(objectInteraction.InteractionObjectData.Boolean);
    }
}
