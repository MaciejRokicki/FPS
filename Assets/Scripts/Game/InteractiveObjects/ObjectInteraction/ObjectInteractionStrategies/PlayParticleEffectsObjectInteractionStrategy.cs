public class PlayParticleEffectsObjectInteractionStrategy : BaseObjectInteractionStrategy
{
    public PlayParticleEffectsObjectInteractionStrategy(ObjectInteraction objectInteraction) : base(objectInteraction)
    {
    }

    public override void Interact()
    {
        objectInteraction.InteractionObjectData.ParticleSystem.Play();
    }
}
