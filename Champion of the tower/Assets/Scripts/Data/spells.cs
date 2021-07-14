using System;

public class Spells
{
    static public Spell testAttack = new Spell
    {
        maxDistance = 5,
        power = 10,
        actionPointRequired = 2,
    };



    public class Spell
    {
        public int maxDistance { get; set; }
        public int power { get; set; }
        public int actionPointRequired { get; set; }
    }
}

