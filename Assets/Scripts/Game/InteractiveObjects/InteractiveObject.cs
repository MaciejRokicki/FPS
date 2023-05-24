using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField]
    private float health;
    public float Health
    {
        get { return health; }
        set
        {
            health = value;

            if (health <= 0.0f)
            {
                health = 0.0f;
                OnObjectDestroy();
            }
        }
    }
    public InteractiveObjectMaterial material;

    public ObjectInteraction[] onHitObjectInteractions;
    public ObjectInteraction[] onDestroyObjectInteractions;

    private void Start()
    {
        foreach(ObjectInteraction interaction in onHitObjectInteractions)
        {
            interaction.SetStrategy();
        }

        foreach (ObjectInteraction interaction in onDestroyObjectInteractions)
        {
            interaction.SetStrategy();
        }
    }

    public void OnHit(InteractiveObjectMaterial material, float dmg)
    {
        if (material == this.material)
        {
            Health -= dmg;

            foreach(ObjectInteraction interaction in onHitObjectInteractions)
            {
                interaction.Strategy.Interact();
            }
        }
    }

    public void OnObjectDestroy()
    {
        foreach (ObjectInteraction interaction in onDestroyObjectInteractions)
        {
            interaction.Strategy.Interact();
        }
    }
}
