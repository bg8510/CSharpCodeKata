using System.Collections.Generic;
using System.Linq;                                                  // Brooks

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
                    new Award {Name = "Blue Star", ExpiresIn = 30, Quality = 50}
                }

            };

            while (true)                                                         // Brooks debug
            {
                app.UpdateQuality();

                System.Console.ReadKey();
            }

        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Awards.Count; i++)
            {
                if (Awards[i].Name != "Blue First" && Awards[i].Name != "Blue Compare")
                {
                    if (Awards[i].Quality > 0)
                    {
                        if (Awards[i].Name != "Blue Distinction Plus")
                        {
                            Awards[i].Quality = Awards[i].Quality - 1;
                        }

                        // Blue Star gets an extra point docked
                        if (Awards[i].Name == "Blue Star")
                        {
                            Awards[i].Quality = Awards[i].Quality - 1;
                        }
                    }
                }
                // This 'else' applies to Blue First and Blue Compare, the two increasers
                else
                {
                    if (Awards[i].Quality < 50)
                    {
                        Awards[i].Quality = Awards[i].Quality + 1;

                        // Blue Compare gets 2 more tiers, at 10 days and at 5 days
                        if (Awards[i].Name == "Blue Compare")
                        {
                            if (Awards[i].ExpiresIn < 11)
                            {
                                if (Awards[i].Quality < 50)
                                {
                                    Awards[i].Quality = Awards[i].Quality + 1;
                                }
                            }

                            if (Awards[i].ExpiresIn < 6)
                            {
                                if (Awards[i].Quality < 50)
                                {
                                    Awards[i].Quality = Awards[i].Quality + 1;
                                }
                            }
                        }
                    }
                }

                // Blue Distinction Plus doesn't decrease in Quality, just counts down the days
                if (Awards[i].Name != "Blue Distinction Plus")
                {
                    Awards[i].ExpiresIn = Awards[i].ExpiresIn - 1;
                }

                // When ExpiresIn goes negative, apply the extra decrease
                if (Awards[i].ExpiresIn < 0)
                {
                    if (Awards[i].Name != "Blue First")
                    {
                        if (Awards[i].Name != "Blue Compare")
                        {
                            if (Awards[i].Quality > 0)
                            {
                                if (Awards[i].Name != "Blue Distinction Plus")
                                {
                                    Awards[i].Quality = Awards[i].Quality - 1;
                                }

                                // Blue Star gets an extra point docked
                                if (Awards[i].Name == "Blue Star")
                                {
                                    Awards[i].Quality = Awards[i].Quality - 1;
                                }
                            }
                        }
                        else
                        {
                            Awards[i].Quality = Awards[i].Quality - Awards[i].Quality;
                        }
                    }
                    else
                    {
                        if (Awards[i].Quality < 50)
                        {
                            Awards[i].Quality = Awards[i].Quality + 1;
                        }
                    }
                }

                //switch (Awards[i].Name)
                //    {
                //        case "Blue First":
                //            if (Awards[i].Quality < 50)
                //            {
                //                Awards[i].Quality++;
                //            }
                //            break;

                //        case "Blue Distinction Plus":
                //            break;

                //        case "Blue Compare":
                //            break;

                //        case "Blue Star":
                //            break;

                //        case "Gov Quality Plus":
                //        case "ACME Partner Facility":
                //        case "Top Connected Providers":

                //            break;

                //        default:
                //            break;
                //    }


            }


            ///////////////////// Brooks - for debug only
            IEnumerable<Award> sortedEnum = Awards.OrderBy(award => award.Name);
            IList<Award> sortedAwardList = sortedEnum.ToList();

            foreach (Award a in sortedAwardList)
            {
                System.Console.WriteLine("\n" + a.Name + "\n Exp  " + a.ExpiresIn + "\t Q " + a.Quality);
            }

            /////////////////////
        }

    }

}
