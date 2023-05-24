public class SetInteractiveObjectHealthObjectInteractionStrategy : BaseObjectInteractionStrategy
{
    public SetInteractiveObjectHealthObjectInteractionStrategy(ObjectInteraction objectInteraction) : base(objectInteraction)
    {
    }

    public override void Interact()
    {
        objectInteraction.InteractionObjectData.InteractiveObject.Health = objectInteraction.InteractionObjectData.Float;
    }
}
