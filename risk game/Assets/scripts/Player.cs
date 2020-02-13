using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int numberOfCountries { get; set; }
    public List<int> countries { get; set; }
    public Material matrial { get; set; }
    public int soldiers_of_draft { get; set; }
    public Player()
    {
        countries = new List<int>();
    }
}
