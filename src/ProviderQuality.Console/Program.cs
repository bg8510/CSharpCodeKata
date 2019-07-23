using System.Collections.Generic;

namespace ProviderQuality.Console
{
    public class Program
    {
        public IList<Award> Awards
        {
            get;
            set;
        }

        static void Main(string[] args)
        {
            System.Console.WriteLine("Updating award metrics...!");

            var app = new Program()
            {
                Awards = new List<Award>
                {
                    new Award {Name = "Gov Quality Plus", ExpiresIn = 10, Quality = 20},
                    new Award {Name = "Blue First", ExpiresIn = 2, Quality = 0},
                    new Award {Name = "ACME Partner Facility", ExpiresIn = 5, Quality = 7},
                    new Award {Name = "Blue Distinction Plus", ExpiresIn = 0, Quality = 80},
                    new Award {Name = "Blue Compare", ExpiresIn = 15, Quality = 20},
                    new Award {Name = "Top Connected Providers", ExpiresIn = 3, Quality = 6},
                    new Award {Name = "Blue Star", ExpiresIn = 10, Quality = 50}
                }

            };

                app.UpdateQuality();

                System.Console.ReadKey();
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Awards.Count; i++)
            {
                switch (Awards[i].Name)
                {
                    case "Blue Distinction Plus":
                        // This case is necessary to prevent "Blue Distinction Plus" from going to the default case
                        break;

                    case "Blue First":
                        if (Awards[i].Quality < 50)
                        {
                            Awards[i].Quality++;
                        }

                        if ((Awards[i].ExpiresIn <= 0) && Awards[i].Quality < 50)
                        {
                            Awards[i].Quality++;
                        }

                        break;

                    case "Blue Compare":
                        if (Awards[i].ExpiresIn <= 0)
                        {
                            Awards[i].Quality = 0;
                        }
                        else if (Awards[i].Quality < 50)
                        {
                            Awards[i].Quality++;

                            // Blue Compare gets 2 more tiers, at 10 days and at 5 days
                            if (Awards[i].ExpiresIn < 11 && Awards[i].Quality < 50)
                            {
                                Awards[i].Quality++;
                            }

                            if (Awards[i].ExpiresIn < 6 && Awards[i].Quality < 50)
                            {
                                Awards[i].Quality++;
                            } 
                        }

                        break;

                    case "Blue Star":
                        if (Awards[i].Quality == 1)
                        {
                            Awards[i].Quality = 0;
                        }

                        if (Awards[i].Quality > 1)
                        {
                            Awards[i].Quality -= 2;
                        }

                        if ((Awards[i].ExpiresIn <= 0))
                        {
                            // Subtract 2 if possible, otherwise it stops at zero
                            if (Awards[i].Quality > 1)
                            {
                                Awards[i].Quality -= 2;
                            }
                            else
                            {
                                Awards[i].Quality = 0;
                            }
                        }

                        break;

                    // These cases aren't necessary, they're here for clarity
                    case "Gov Quality Plus":
                    case "ACME Partner Facility":
                    case "Top Connected Providers":
                    default:
                        if (Awards[i].Quality > 0)
                        {
                            Awards[i].Quality--;
                        }

                        if (Awards[i].Quality > 0 && Awards[i].ExpiresIn <= 0)
                        {
                            Awards[i].Quality--;
                        }
                        
                        break;
                }

                Awards[i].ExpiresIn--;

            }
        }
    }
}
