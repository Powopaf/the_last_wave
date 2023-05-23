using System;
using System.Collections;
using System.Collections.Generic;


public class Farming
{
    public int number;
    public string type;

    public Farming(string type)
    {
        if (type == "Tree")
        {
            this.type = "Tree";
            number = 10;
        }
        else if (type == "Rock")
        {
            this.type = "Rock";
            number = 5;
        }
        else
        {
            throw new ArgumentException("The type doesn't exist, the Farming are Gold Rock Tree");
        }
    }
}
