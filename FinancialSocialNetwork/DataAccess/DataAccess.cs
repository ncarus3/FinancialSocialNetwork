using System.Collections;
using System.Data.SqlClient;
using System.Runtime.ConstrainedExecution;

namespace FinancialSocialNetwork.DataAccess
{
    public class DataAccess //Our file to access SQL information. Here we will have our stored procedures and functions
    {

    

        private SqlConnection con;

        private void connection()
        {
            // String constr = ConfigurationManager.ConnectionStrings[""].ToString;

            // String constr = "Server=(localdb)\\mssqllocaldb;Database=aspnet-53bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;MultipleActiveResultSets=true";
            String constr = "Data Source=LAPTOP-R3AO3BS2\\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            
            con = new SqlConnection(constr);
            


        }


        public List<String> getUsers()
        {
            connection();
            SqlCommand command = new SqlCommand("SELECT * FROM Users", con);
            try
            {
                con.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to connect to DB:  " + e.ToString()); //not handling DB errors for this project just yet.
            }

            using (SqlDataReader reader = command.ExecuteReader())
            {
                //create a model for users
                while (reader.Read())
                {
                    //users.name = reader["name"].ToString();
                }
            }

                return null;
        }

    }
}
