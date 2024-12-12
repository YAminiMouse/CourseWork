using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HM2.AdditionalEntities
{
    public class TopThreeReport
    {
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
        }
        private string secondName;
        public string SecondName
        {
            get
            {
                return secondName;
            }
        }
        private string thirdName;
        public string ThirdName
        {
            get
            {
                return thirdName;
            }
        }
        private int firstCost;
        public int FirstCost
        {
            get
            {
                return firstCost;
            }
        }
        private int secondCost;
        public int SecondCost
        {
            get
            {
                return secondCost;
            }
        }
        private int thirdcost;
        public int ThirdCost
        {
            get
            {
                return thirdcost;
            }
        }
        public TopThreeReport(int fisrtCost , int secondCost , int thirdCost , string firstName , string secondName , string thirdName)
        {
            this.firstCost = fisrtCost;
            this.secondCost = secondCost;
            this.thirdcost = thirdCost;
            this.firstName = firstName;
            this.secondName = secondName;
            this.thirdName = thirdName;
        }
    }
}
