public class PlayAnimationObjectInteractionStrategy : BaseObjectInteractionStrategy
{
    public PlayAnimationObjectInteractionStrategy(ObjectInteraction objectInteraction) : base(objectInteraction)
    {
    }

    public override void Interact()
    {
        objectInteraction.InteractionObjectData.Animator.Play(objectInteraction.InteractionObjectData.String);
    }
}
