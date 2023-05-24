using System;

[Serializable]
public class ObjectInteraction
{
    public ObjectInteractionType Type;
    public InteractionObjectData InteractionObjectData;
    public BaseObjectInteractionStrategy Strategy;

    public void SetStrategy()
    {
        switch (Type)
        {
            case ObjectInteractionType.DebugLog:
                Strategy = new DebugLogObjectInteractionStrategy(this);
                break;

            case ObjectInteractionType.SetActive:
                Strategy = new SetActiveObjectInteractionStrategy(this);
                break;

            case ObjectInteractionType.Translate:
                Strategy = new TranslateObjectInteractionStrategy(this);
                break;

            case ObjectInteractionType.Rotate:
                Strategy = new RotateObjectInteractionStrategy(this);
                break;

            case ObjectInteractionType.ChangeMaterial:
                Strategy = new ChangeMaterialObjectInteractionStrategy(this);
                break;

            case ObjectInteractionType.PlayParticleEffects:
                Strategy = new PlayParticleEffectsObjectInteractionStrategy(this);
                break;

            case ObjectInteractionType.PlayAnimation:
                Strategy = new PlayAnimationObjectInteractionStrategy(this);
                break;

            case ObjectInteractionType.SetInteractiveObjectHealth:
                Strategy = new SetInteractiveObjectHealthObjectInteractionStrategy(this);
                break;

            default:
                Strategy = null;
                break;
        }
    }
}
