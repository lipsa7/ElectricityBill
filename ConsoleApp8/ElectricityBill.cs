using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillAutomation    //DO NOT change the namespace name
{
    public class ElectricityBill         //DO NOT change the class name
    {
        //Implement the fields and properties as per description

        private string consumerNumber;
        private string consumerName;
        private int unitsConsumed;
        private double billAmount;

        public string ConsumerNumber{get=>consumerNumber; set=>consumerNumber=value;}
        public string ConsumerName { get=>consumerName; set=>consumerName=value; }
        public int UnitsConsumed { get=>unitsConsumed; set=>unitsConsumed=value; }
        public double BillAmount { get=>billAmount; set=>billAmount=value; }

        
    }
}
