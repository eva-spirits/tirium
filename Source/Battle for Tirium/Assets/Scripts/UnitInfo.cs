using UnityEngine;

/// <summary>
/// Object used to represent an in-game unit.
/// </summary>
[CreateAssetMenu(fileName = "New Unit", menuName = "Unit")]
public class UnitInfo : ScriptableObject
{
    /// <summary>
    /// Name of the unit.
    /// </summary>
    public string unitName;

    /// <summary>
    /// Unit's health.
    /// </summary>
    public int health;

    /// <summary>
    /// Unit's damage on attack.
    /// </summary>
    public int damage;

    /// <summary>
    /// Model used for the unit.
    /// </summary>
    public Mesh model;
}
