using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Round
{
    public List<Wave> waves = new List<Wave>();
    public float timeBetweenEachWave;
    public int howManyTimesToRepeat;
}


/*
 * list of waves that have howmanybloonsperspawn, timebetweeneachone, and bloontype
 * float that is timebetweeneachwave
 * 
 * 
 * 
 * 
 * 
 * 
 */