public class TranslateObjectInteractionStrategy : BaseObjectInteractionStrategy
{
    public TranslateObjectInteractionStrategy(ObjectInteraction objectInteraction) : base(objectInteraction)
    {
    }

    public override void Interact()
    {
        objectInteraction.InteractionObjectData.Transform.transform.Translate(objectInteraction.InteractionObjectData.Vector3);
    }
}
