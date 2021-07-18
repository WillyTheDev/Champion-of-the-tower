using System;

public class Spells
{
    static public Spell testAttack = new Spell
    {
        maxDistance = 5,
        minDistance = 0,
        power = 10,
        actionPointRequired = 2,
    };

    static public Spell testDistanceAttack = new Spell
    {
        maxDistance = 25,
        minDistance = 15,
        power = 15,
        actionPointRequired = 3,
    };


    public class Spell
    {
        public int maxDistance { get; set; }
        public int minDistance { get; set; }
        public int power { get; set; }
        public int actionPointRequired { get; set; }
    }
}

