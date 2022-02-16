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
        public bool IsOrNotEqual(int int1, int int2)
        {
            if(int1 == int2)
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
            if(int1 > int2)
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
