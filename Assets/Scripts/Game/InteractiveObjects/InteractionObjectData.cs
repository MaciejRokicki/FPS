using System;
using UnityEngine;

[Serializable]
public class InteractionObjectData
{
    public GameObject GameObject;
    public Transform Transform;
    public MeshRenderer MeshRenderer;
    public ParticleSystem ParticleSystem;
    public Animator Animator;
    public InteractiveObject InteractiveObject;

    public string String;
    public bool Boolean;
    public float Float;
    public Vector3 Vector3;
    public Material Material;
}