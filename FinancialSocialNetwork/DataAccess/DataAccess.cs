using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;

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


        public List<Models.UserModel> getUsers()
        {
            connection();
            SqlCommand command = new SqlCommand("SELECT * FROM Users", con);
            List<Models.UserModel> users = new List<Models.UserModel>();

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
                while (reader.Read()) {
                    Models.UserModel user = new Models.UserModel();

                    user.userID = Int32.Parse(reader["UserID"].ToString());
                    user.email = reader["Email"].ToString();
                    user.phoneNumber = (reader["PhoneNumber"].ToString());
                    user.firstName = reader["FirstName"].ToString();
                    user.lastName = reader["LastName"].ToString();
                    user.country = reader["Country"].ToString();
                    user.username = reader["Username"].ToString();

                    //users.name = reader["name"].ToString();
                }
            }

                return null;
        }


        public Boolean login(String username, String password)
        {

            Boolean success = false;
            connection();

            String passwordHashed = getPasswordHash(password);

            SqlCommand command = new SqlCommand("SELECT UserID FROM UserLogin WHERE Username='" + username + "' AND Password='" + passwordHashed + "'", con);
            try
            {
                con.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to connect to DB:  " + e.ToString()); //not handling DB errors for this project just yet.
            }
            int r = command.ExecuteNonQuery();

            if (r == 1)
                success= true;
            return success;

        }

        private string getPasswordHash(string password) //using base64 old method just for functionlity. NOT SECURE anymore
        {
            String hashedPassword = "";
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(password);
            hashedPassword = System.Convert.ToBase64String(plainTextBytes);


            return hashedPassword;
        }

        public String getBio(int UserID)
        {
            connection();
            SqlCommand command = new SqlCommand("SELECT Bio FROM UserInfo WHERE UserID=" + UserID, con);
            try {
                con.Open();
            } catch (Exception e) {
                Console.WriteLine("Failed to connect to DB:  " + e.ToString()); //not handling DB errors for this project just yet.
            }

            String bio = "";

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {

                    bio = reader["Bio"].ToString();
                }
            }

            if (bio == null)
            {
                bio = "I don't have a bio just yet!";
            }

            return bio;
        }
    }
}
