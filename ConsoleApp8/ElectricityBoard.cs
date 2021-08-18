using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace BillAutomation      //DO NOT change the namespace name
{
    public class ElectricityBoard  //DO NOT change the class name
    {
        //Implement the property as per the description

        //Implement the methods as per the description   

        public SqlConnection SqlCon { get { return DBHandler.GetConnection(); } }

        public void AddBill(ElectricityBill ebill)
        {
            try
            {
                SqlCommand scmd = new SqlCommand("insert into ElectricityBill values(@CONNO,@CONNAME,@UNITSCON,@BILLAMO)", SqlCon);
                //string query = "insert into ElectricityBill (consumer_number, consumer_name, units_consumed, bill_amount) values(ebill.ConsumerNumber,ebill.ConsumerName,ebill.UnitsConsumed,ebill.BillAmount)";
                //using (SqlCommand scmd = new SqlCommand(query, scon))
                //{
                scmd.Parameters.AddWithValue("@CONNO", ebill.ConsumerNumber);
                scmd.Parameters.AddWithValue("@CONNAME", ebill.ConsumerName);
                scmd.Parameters.AddWithValue("@UNITSCON", ebill.UnitsConsumed);
                scmd.Parameters.AddWithValue("@BILLAMO", ebill.BillAmount);
                //scmd.CommandType = System.Data.CommandType.Text;
                scmd.ExecuteNonQuery();
            }
            catch (SqlException E)
            {
                Console.WriteLine(E.Message);
            }
            finally
            {
                SqlCon.Close();
            }
        }
                    //scon.Open();
                    //int i = (Int32)scmd.ExecuteScalar();
                    //scon.Close();

                    //return i;
                //}
            //}
            
            
       // }

        public void CalculateBill(ElectricityBill ebill)
        {
            double TotalBill = 0;
            int units = ebill.UnitsConsumed;

            if (units <= 100)
                TotalBill = 0;
            else if (units <= 300)
                TotalBill = 1.5 * (units-100);
            else if (units <= 600)
                TotalBill = 200*1.5 + 6.5 * (units-300);
            else if (units <= 1000)
                TotalBill = 200*1.5 + 300*3.5 + 5.5 * (units-600);
            else
                TotalBill = 200*1.5 + 300*3.5 + 5.5*400 + 7.5 * (units-1000);

            Console.WriteLine("Bill Amount is: "+TotalBill);
            ebill.BillAmount = TotalBill;

        }

        public List<ElectricityBill> Generate_N_BillDetails(int num)
        {
            List<ElectricityBill> blist = new List<ElectricityBill>();
            ElectricityBill ebill = null;
            try
            {
                SqlCommand scmd = new SqlCommand("select top " + num + "* from ElectricityBill order by consumer_number", SqlCon);
                SqlDataReader R = scmd.ExecuteReader();
                if (R.HasRows)
                {
                    while (R.Read())
                    {
                        ebill = new ElectricityBill();
                        ebill.ConsumerNumber = R["consumer_number"].ToString();
                        ebill.ConsumerName = R["consumer_name"].ToString();
                        ebill.UnitsConsumed = int.Parse(R["units_consumed"].ToString());
                        ebill.BillAmount = double.Parse(R["bill_amount"].ToString());
                        blist.Add(ebill);
                    }
                }
            }
            catch(SqlException E)
            {
                Console.WriteLine(E.Message);
            }

            finally
            {
                SqlCon.Close();
            }

            return blist;
        }
    }
}
