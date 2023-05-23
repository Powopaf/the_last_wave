using System;
using System.Collections;
using System.Collections.Generic;


public class Farming
{
    public int number;
    public string type;
    public Random Random = new Random();

    public Farming(string type)
    {
        if (type == "Tree")
        {
            this.type = "Tree";
            number =Random.Next(30,50);
        }
        else if (type == "Rock")
        {
            this.type = "Rock";
            number = Random.Next(10,30);
        }
        else if (type == "Gold")
        {
            this.type = "Gold";
            number = Random.Next(5,15);
        }
        else
        {
            throw new ArgumentException("The type doesn't exist, the Farming are Gold Rock Tree");
        }
    }
}
