using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Unit")]
public class CharacterInfo : ScriptableObject
{
    public string unitName;
    public int health;
    public int damage;
    public Mesh model;
}
