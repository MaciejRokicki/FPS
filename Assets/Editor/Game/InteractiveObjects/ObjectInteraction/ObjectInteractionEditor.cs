using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ObjectInteraction))]
public class ObjectInteractionPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty typeSerializedProperty = property.FindPropertyRelative("Type");
        ObjectInteractionType type = (ObjectInteractionType)typeSerializedProperty.enumValueFlag;
        EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), typeSerializedProperty);

        SerializedProperty gameObjectSerializedProperty;
        Rect gameObjectSerializedPropertyRect;
        SerializedProperty transformSerializedProperty;
        Rect transformSerializedPropertyRect;
        SerializedProperty meshRendererSerializedProperty;
        Rect meshRendererSerializedPropertyRect;
        SerializedProperty particleSystemSerializedProperty;
        Rect particleSystemSerializedPropertyRect;
        SerializedProperty animatorSerializedProperty;
        Rect animatorSerializedPropertyRect;
        SerializedProperty interactiveObjectSerializedProperty;
        Rect interactiveObjectSerializedPropertyRect;

        SerializedProperty stringSerializedProperty;
        Rect stringSerializedPropertyRect;
        SerializedProperty booleanSerializedProperty;
        Rect booleanSerializedPropertyRect;
        SerializedProperty floatSerializedProperty;
        Rect floatSerializedPropertyRect;
        SerializedProperty vector3SerializedProperty;
        Rect vector3SerializedPropertyRect;
        SerializedProperty materialSerializedProperty;
        Rect materialSerializedPropertyRect;

        switch (type)
        {
            case ObjectInteractionType.DebugLog:
                stringSerializedProperty = property.FindPropertyRelative("InteractionObjectData").FindPropertyRelative("String");
                stringSerializedPropertyRect = new Rect(position.x, position.y + 20f, position.width, EditorGUIUtility.singleLineHeight);

                stringSerializedProperty.stringValue = EditorGUI.TextField(stringSerializedPropertyRect, "Message", stringSerializedProperty.stringValue);
                break;

            case ObjectInteractionType.SetActive:
                gameObjectSerializedProperty = property.FindPropertyRelative("InteractionObjectData").FindPropertyRelative("GameObject");
                booleanSerializedProperty = property.FindPropertyRelative("InteractionObjectData").FindPropertyRelative("Boolean");

                gameObjectSerializedPropertyRect = new Rect(position.x, position.y + 20f, position.width, EditorGUIUtility.singleLineHeight);
                booleanSerializedPropertyRect = new Rect(position.x, position.y + 40f, position.width, EditorGUIUtility.singleLineHeight);

                EditorGUI.ObjectField(gameObjectSerializedPropertyRect, gameObjectSerializedProperty, typeof(GameObject));
                booleanSerializedProperty.boolValue = EditorGUI.Toggle(booleanSerializedPropertyRect, "Set active: ", booleanSerializedProperty.boolValue);
                break;

            case ObjectInteractionType.Translate:
                transformSerializedProperty = property.FindPropertyRelative("InteractionObjectData").FindPropertyRelative("Transform");
                vector3SerializedProperty = property.FindPropertyRelative("InteractionObjectData").FindPropertyRelative("Vector3");

                transformSerializedPropertyRect = new Rect(position.x, position.y + 20f, position.width, EditorGUIUtility.singleLineHeight);
                vector3SerializedPropertyRect = new Rect(position.x, position.y + 40f, position.width, EditorGUIUtility.singleLineHeight);

                EditorGUI.ObjectField(transformSerializedPropertyRect, transformSerializedProperty, typeof(Transform));
                vector3SerializedProperty.vector3Value = EditorGUI.Vector3Field(vector3SerializedPropertyRect, "Translate: ", vector3SerializedProperty.vector3Value);
                break;

            case ObjectInteractionType.Rotate:
                transformSerializedProperty = property.FindPropertyRelative("InteractionObjectData").FindPropertyRelative("Transform");
                vector3SerializedProperty = property.FindPropertyRelative("InteractionObjectData").FindPropertyRelative("Vector3");

                transformSerializedPropertyRect = new Rect(position.x, position.y + 20f, position.width, EditorGUIUtility.singleLineHeight);
                vector3SerializedPropertyRect = new Rect(position.x, position.y + 40f, position.width, EditorGUIUtility.singleLineHeight);

                EditorGUI.ObjectField(transformSerializedPropertyRect, transformSerializedProperty, typeof(Transform));
                vector3SerializedProperty.vector3Value = EditorGUI.Vector3Field(vector3SerializedPropertyRect, "Translate: ", vector3SerializedProperty.vector3Value);
                break;

            case ObjectInteractionType.ChangeMaterial:
                meshRendererSerializedProperty = property.FindPropertyRelative("InteractionObjectData").FindPropertyRelative("MeshRenderer");
                materialSerializedProperty = property.FindPropertyRelative("InteractionObjectData").FindPropertyRelative("Material");

                meshRendererSerializedPropertyRect = new Rect(position.x, position.y + 20f, position.width, EditorGUIUtility.singleLineHeight);
                materialSerializedPropertyRect = new Rect(position.x, position.y + 40f, position.width, EditorGUIUtility.singleLineHeight);

                EditorGUI.ObjectField(meshRendererSerializedPropertyRect, meshRendererSerializedProperty, typeof(MeshRenderer));
                EditorGUI.ObjectField(materialSerializedPropertyRect, materialSerializedProperty, typeof(Material));
                break;

            case ObjectInteractionType.PlayParticleEffects:
                particleSystemSerializedProperty = property.FindPropertyRelative("InteractionObjectData").FindPropertyRelative("ParticleSystem");

                particleSystemSerializedPropertyRect = new Rect(position.x, position.y + 20f, position.width, EditorGUIUtility.singleLineHeight);

                EditorGUI.ObjectField(particleSystemSerializedPropertyRect, particleSystemSerializedProperty, typeof(ParticleSystem));
                break;

            case ObjectInteractionType.PlayAnimation:
                animatorSerializedProperty = property.FindPropertyRelative("InteractionObjectData").FindPropertyRelative("Animator");
                stringSerializedProperty = property.FindPropertyRelative("InteractionObjectData").FindPropertyRelative("String");

                animatorSerializedPropertyRect = new Rect(position.x, position.y + 20f, position.width, EditorGUIUtility.singleLineHeight);
                stringSerializedPropertyRect = new Rect(position.x, position.y + 40f, position.width, EditorGUIUtility.singleLineHeight);

                EditorGUI.ObjectField(animatorSerializedPropertyRect, animatorSerializedProperty, typeof(Animator));
                stringSerializedProperty.stringValue = EditorGUI.TextField(stringSerializedPropertyRect, "Animation name", stringSerializedProperty.stringValue);
                break;

            case ObjectInteractionType.SetInteractiveObjectHealth:
                interactiveObjectSerializedProperty = property.FindPropertyRelative("InteractionObjectData").FindPropertyRelative("InteractiveObject");
                floatSerializedProperty = property.FindPropertyRelative("InteractionObjectData").FindPropertyRelative("Float");

                interactiveObjectSerializedPropertyRect = new Rect(position.x, position.y + 20f, position.width, EditorGUIUtility.singleLineHeight);
                floatSerializedPropertyRect = new Rect(position.x, position.y + 40f, position.width, EditorGUIUtility.singleLineHeight);

                EditorGUI.ObjectField(interactiveObjectSerializedPropertyRect, interactiveObjectSerializedProperty, typeof(InteractiveObject));
                floatSerializedProperty.floatValue = EditorGUI.FloatField(floatSerializedPropertyRect, "Health", floatSerializedProperty.floatValue);
                break;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) + 50;
    }
}