public class RotateObjectInteractionStrategy : BaseObjectInteractionStrategy
{
    public RotateObjectInteractionStrategy(ObjectInteraction objectInteraction) : base(objectInteraction)
    {
    }

    public override void Interact()
    {
        objectInteraction.InteractionObjectData.Transform.transform.Rotate(objectInteraction.InteractionObjectData.Vector3);
    }
}
