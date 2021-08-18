using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;

namespace BillAutomation         //DO NOT change the namespace name
{
    public class Program        //DO NOT change the class name
    {

        static void Main(string[] args)  //DO NOT change the 'Main' method signature
        {
            //Implement the code here
            ElectricityBill e = new ElectricityBill();

            ElectricityBoard EB = new ElectricityBoard();

            BillValidator B = new BillValidator();
            //B.ValidateUnitsConsumed(e.UnitsConsumed);

            List<ElectricityBill> blist = new List<ElectricityBill>();

            Console.WriteLine("Enter the number of bills to be added");
            int numberOfBills = int.Parse(Console.ReadLine());

            ElectricityBill[] Ebill = new ElectricityBill[numberOfBills];

            try
            {
                for (int i = 0; i < numberOfBills; i++)
                {
                    Ebill[i] = new ElectricityBill();
                    Console.WriteLine("Enter consumer number");

                    string cno = Console.ReadLine();
                    Regex rg = new Regex("^EB[0-9]{5}$");

                    if (!rg.IsMatch(cno))
                        throw new FormatException("\nInvalid Consumer Number");

                    Ebill[i].ConsumerNumber = cno;

                    Console.WriteLine("Enter consumer name");
                    string cname = Console.ReadLine();

                    Ebill[i].ConsumerName = cname;

                    Console.WriteLine("Enter units consumed");
                    int uc = int.Parse(Console.ReadLine());

                    string mesg = B.ValidateUnitsConsumed(uc);

                    while (!(string.IsNullOrEmpty(mesg)))
                    {
                        Console.WriteLine(mesg);
                        Console.WriteLine("Enter units consumed");
                        uc = int.Parse(Console.ReadLine());

                        mesg = B.ValidateUnitsConsumed(uc);
                    }

                    Ebill[i].UnitsConsumed = uc;
                
                }
                Console.WriteLine("Enter last N number of bills to generate: ");
                int nobg = int.Parse(Console.ReadLine());
                for(int i = 0; i < Ebill.Length; i++)
                {
                    Console.WriteLine(Ebill[i].ConsumerNumber);
                    Console.WriteLine(Ebill[i].ConsumerName);
                    Console.WriteLine(Ebill[i].UnitsConsumed);

                    EB.CalculateBill(Ebill[i]);
                    EB.AddBill(Ebill[i]);
                }

                Console.WriteLine("Details of last N bills:");
                blist = EB.Generate_N_BillDetails(nobg);

                for(int i = 0; i < blist.Count; i++)
                {
                    Console.WriteLine("EB Bill for " + blist[i].ConsumerName + " is " + blist[i].BillAmount);
                }
            }

            catch(FormatException E)
            {
                Console.WriteLine(E.Message);
            }

            Console.Read();
            

            //Console.WriteLine("Enter consumer number");
            //var consunum = Convert.ToString(Console.ReadLine());

            //var item=Convert.ToInt32(consunum.Substring(2, 5));
            //var valid2 = item is int;
            
            //if ((consunum.Substring(0, 2) == "EB") && valid2) 
            //{
            //    e.ConsumerNumber = consunum; 
            //}
            //else
            //{
            //    Console.WriteLine("Invalid Consumer Number");
            //}

            //Console.WriteLine("Enter consumer name");
            //e.ConsumerName = Convert.ToString(Console.ReadLine());


            //bool valid = false;

            //while (!valid)
            //{
            //    Console.WriteLine("Enter units consumed");
            //    e.UnitsConsumed = int.Parse(Console.ReadLine());
            //    if (e.UnitsConsumed > 0)
            //        valid = true;
            //    else
            //        Console.WriteLine("Given units is invalid");
                
            //}
            ////Console.WriteLine("Enter units consumed");
            ////e.UnitsConsumed = int.Parse(Console.ReadLine());

            //Console.WriteLine("Enter last n number of bills to generate");
            //int n = int.Parse(Console.ReadLine());

            
            
        }
}

public class BillValidator
{      //DO NOT change the class name

    public String ValidateUnitsConsumed(int UnitsConsumed)      //DO NOT change the method signature
    {
        //Implement code here
        if (UnitsConsumed < 0)
            return "Given units is invalid";
        else
            return null;
    }
}
}
