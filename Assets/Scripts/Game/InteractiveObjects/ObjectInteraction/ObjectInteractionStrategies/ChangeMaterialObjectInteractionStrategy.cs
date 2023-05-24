public class ChangeMaterialObjectInteractionStrategy : BaseObjectInteractionStrategy
{
    public ChangeMaterialObjectInteractionStrategy(ObjectInteraction objectInteraction) : base(objectInteraction)
    {
    }

    public override void Interact()
    {
        objectInteraction.InteractionObjectData.MeshRenderer.material = objectInteraction.InteractionObjectData.Material;
    }
}
