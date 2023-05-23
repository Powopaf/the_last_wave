using System;

namespace Players.Farming
{
    public class Farming
    {
        public readonly int Number;

        public Farming(string type)
        {
            Number = type switch
            {
                "Tree" => 10,
                "Rock" => 5,
                _ => throw new ArgumentException("The type doesn't exist, the Farming are Gold Rock Tree")
            };
        }
    }
}
