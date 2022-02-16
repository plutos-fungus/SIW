using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIW
{
    public class OperationManager
    {
        // A suggestion to an comparison operation manager
        private string tempName;

        // Check if something is a string or an int
        public void IsType(string string1, string string2, string opperation, int int1, int int2)
        {
            if (int.TryParse(string1, out int1) && int.TryParse(string2, out int2))
            {
                tempName = "I";
                ChooserInt(tempName, opperation, int1, int2);
            }
            else
            {
                tempName = "S";
                ChooserString(tempName, opperation, string1, string2);
            }
        }

        public void ChooserInt(string tempName, string opperation, int int1, int int2)
        {
            // If tempName is "I", then the type is an int
            if (opperation == "==")
            {
                IsOrNotEqual(int1, int2);
            }
            else if (tempName == "I" && opperation == ">")
            {
                IsOrNotBigger(int1, int2);
            }
            else if (tempName == "I" && opperation == ">=")
            {
                IsBiggerOrEqualTo(int1, int2);
            }
        }
    
        public void ChooserString(string tempName, string opperation, string string1, string string2)
        {
            // Do string comparison opperation here
        }

        public bool IsOrNotEqual(int int1, int int2)
        {
            if (int1 == int2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsOrNotBigger(int int1, int int2)
        {
            if (int1 > int2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsBiggerOrEqualTo(int int1, int int2)
        {
            if (int1 >= int2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
