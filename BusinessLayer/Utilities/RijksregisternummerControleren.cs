using BusinessLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Utilities
{
    public class RijksregisternummerControleren
    {
        public static bool ValidatieRijkregisternummer(string rijksregisternr)
        { // Zou correct moeten zijn.
            try
            {
                if (string.IsNullOrWhiteSpace(rijksregisternr))
                {
                    throw new BestuurderException("Rijksregisternummer mag niet leeg zijn.");
                }
                else if (rijksregisternr.Length != 11)
                {
                    throw new BestuurderException("Rijksregisternummer moet uit 11 cijfers bestaan.");
                }
                else
                {
                    int laatste2 = int.Parse(rijksregisternr.Substring(9));
                    int eerste9 = int.Parse(rijksregisternr.Substring(0, 9));
                    if (laatste2 != (97 - (eerste9 % 97)))
                    {
                        throw new BestuurderException("Rijksregisternummer ongeldig");
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch
            {
                // Display exception
                return false;
            }
        }
    }
}
