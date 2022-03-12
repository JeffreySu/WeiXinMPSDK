public class Women
{
    public byte Age { get; private set; }

    public Women()
    {
        Age = 0;
    }

    public void YearPass()
    {
        if (Age < 18)
        {
            Age++;
        } 
        else
        {
            lock(Age)
            {
                while(true)
                {
                    Console.Write("Women are Amazing!");
                }
            }
        }
    }
}


