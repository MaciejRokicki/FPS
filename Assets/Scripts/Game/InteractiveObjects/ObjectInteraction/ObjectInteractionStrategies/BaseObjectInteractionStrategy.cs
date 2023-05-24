public abstract class BaseObjectInteractionStrategy
{
    protected ObjectInteraction objectInteraction;

    public BaseObjectInteractionStrategy(ObjectInteraction objectInteraction)
    {
        this.objectInteraction = objectInteraction;
    }

    public abstract void Interact();
}