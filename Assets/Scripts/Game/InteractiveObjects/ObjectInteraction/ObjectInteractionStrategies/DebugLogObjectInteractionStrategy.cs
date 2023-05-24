using UnityEngine;

public class DebugLogObjectInteractionStrategy : BaseObjectInteractionStrategy
{
    public DebugLogObjectInteractionStrategy(ObjectInteraction objectInteraction) : base(objectInteraction)
    {
    }

    public override void Interact()
    {
        Debug.Log(objectInteraction.InteractionObjectData.String);
    }
}
