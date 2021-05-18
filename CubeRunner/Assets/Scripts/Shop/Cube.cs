using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cube
{
    [SerializeField] private string name;
    [SerializeField] private int cost;
    [SerializeField] private bool isBought;
    [SerializeField] private bool isEquiped;
    [SerializeField] private PlayerTypes type;

    public Cube(string name, int cost, bool isBought, bool isEquiped, PlayerTypes type)
    {
        this.name = name;
        this.cost = cost;
        this.isBought = isBought;
        this.isEquiped = isEquiped;
        this.type = type;
    }

    public string GetName()
    {
        return name;
    }

    public int GetCost()
    {
        return cost;
    }

    public bool IsBought()
    {
        return isBought;
    }

    public bool IsEquiped()
    {
        return isEquiped;
    }

    public PlayerTypes GetPlayerType()
    {
        return type;
    }

    public void SetBought(bool isBought)
    {
        this.isBought = isBought;
    }

    public void SetEquip(bool isEquiped)
    {
        this.isEquiped = isEquiped;
    }
}
