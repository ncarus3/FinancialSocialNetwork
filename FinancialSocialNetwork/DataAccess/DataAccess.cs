using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using Microsoft.AspNetCore.Http;


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
            SqlCommand command = new SqlCommand("dbo.GetUserData", con);
            command.CommandType = CommandType.StoredProcedure;
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
                    user.photoURL = reader["PhotoURL"].ToString();
                    user.bio = reader["Bio"].ToString();
                    user.bankIDs= reader["BankIDs"].ToString();
                    users.Add(user);
                    //users.name = reader["name"].ToString();
                }
            }
            
                return users;
        }

        public Boolean login(String username, String password, out String? UserID)
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
            String tmp = "";
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    success = true;

                }
                while (reader.Read()) { 
                      tmp = reader["UserID"].ToString();

                }
            }

            UserID = tmp;
            return success;

        }

        private string getPasswordHash(string password) //using base64 old method just for functionlity. NOT SECURE anymore
        {
            String hashedPassword = "";
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(password);
            hashedPassword = System.Convert.ToBase64String(plainTextBytes);


            return hashedPassword;
        }

        public Boolean updateBio(int UserID, String newBio)//Used for removing too
        {

            if (newBio.Equals("remove"))
                newBio = "I don't have a bio yet!";

            Boolean r = false;
            connection();
            SqlCommand command = new SqlCommand("UPDATE UserInfo SET Bio='" + newBio + "' WHERE UserID=" + UserID);
            try
            {
                con.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to connect to DB:  " + e.ToString()); //not handling DB errors for this project just yet.
            }
            int rInt = command.ExecuteNonQuery();
            if (rInt == 1)
            {
                r = true;
            }
            return r;
        }

        public Boolean updatePicture(int UserID, String photoURL)//Used for removing too
        {

            if (photoURL.Equals("remove"))
                photoURL = "https://us.123rf.com/450wm/urfandadashov/urfandadashov1806/urfandadashov180601827/urfandadashov180601827.jpg?ver=6";
           
            Boolean r = false;
            connection();
            SqlCommand command = new SqlCommand("UPDATE UserInfo SET ProfileURL='"+photoURL+"' WHERE UserID=" + UserID);
            try
            {
                con.Open();
            } catch(Exception e)
            {
                Console.WriteLine("Failed to connect to DB:  " + e.ToString()); //not handling DB errors for this project just yet.
            }
            int rInt = command.ExecuteNonQuery();
            if (rInt == 1)
            {
                r = true;
            }
            return r;
        }

        public Boolean updateBanks(int UserID, String newBanks)//Used for removing too
        {

            if (newBanks.Equals("remove"))
                newBanks = "";
            Boolean r = false;
            connection();
            SqlCommand command = new SqlCommand("UPDATE UserInfo SET Banks='" + newBanks + "' WHERE UserID=" + UserID);
            try
            {
                con.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to connect to DB:  " + e.ToString()); //not handling DB errors for this project just yet.
            }
            int rInt = command.ExecuteNonQuery();
            if (rInt == 1)
            {
                r = true;
            }
            return r;
        }

        public String getProfile(int UserID)
        {
            connection();
            SqlCommand command = new SqlCommand("SELECT PhotoURL FROM UserInfo WHERE UserID=" + UserID, con);
            try
            {
                con.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to connect to DB:  " + e.ToString()); //not handling DB errors for this project just yet.
            }

            String profileURL = "";

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {

                    profileURL = reader["PhotoURL"].ToString();
                }
            }

            

            return profileURL;
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
