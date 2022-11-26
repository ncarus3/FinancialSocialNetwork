using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Net;
using FinancialSocialNetwork.Models;
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
                    user.banksList = getUserBanks(user.userID);
                    users.Add(user);
                    //users.name = reader["name"].ToString();
                }
            }
            
                return users;
        }

        public List<Models.BankModel> getBanks()
        {
            connection();
            SqlCommand command = new SqlCommand("SELECT * FROM Banks", con);
            List<Models.BankModel> banks = new List<Models.BankModel>();

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
                    Models.BankModel bank = new Models.BankModel();

                    bank.BankID = Int32.Parse(reader["BankID"].ToString());
                    bank.BankName = reader["BankName"].ToString();
                    
                    banks.Add(bank);
                    //users.name = reader["name"].ToString();
                }
            }

            return banks;
        }

        public Models.UserModel getUser(int UserID)
        {

            Models.UserModel user = new Models.UserModel();
            List<UserModel> UM = new List<UserModel>();
            UM = getUsers();

            foreach(Models.UserModel u in UM)
            {
                if (u.userID == UserID)
                {
                    user = u;
                    break;
                } else
                {
                    
                }
            }

            return user;
        }

        public Boolean register(String fName, String lName, String username, String email, String password, String phoneNumber, String Country)
        {
            Boolean r = false;

			connection();
			SqlCommand command = new SqlCommand("dbo.RegisterAccount", con);
			command.CommandType = CommandType.StoredProcedure;

			String passwordHashed = getPasswordHash(password);


			command.Parameters.AddWithValue("username", username);
            command.Parameters.AddWithValue("firstName", fName);
            command.Parameters.AddWithValue("lastName", lName);
            command.Parameters.AddWithValue("email", email);
            command.Parameters.AddWithValue("password", passwordHashed);
            command.Parameters.AddWithValue("country", Country);
            command.Parameters.AddWithValue("phone", phoneNumber);
			try
			{
				con.Open();
			}
			catch (Exception e)
			{
				Console.WriteLine("Failed to connect to DB:  " + e.ToString()); //not handling DB errors for this project just yet.
			}

            int rInt = command.ExecuteNonQuery();

            if (rInt == 3)
            {
                r = true;
            }

            
			return r;

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
                newBio = "I dont have a bio yet!";

            Boolean r = false;
            connection();
            SqlCommand command = new SqlCommand("UPDATE UserInfo SET Bio='" + newBio + "' WHERE UserID=" + UserID, con);
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
                photoURL = "https://thumbs.dreamstime.com/b/no-user-profile-picture-hand-drawn-illustration-53840792.jpg";
           
            Boolean r = false;
            connection();
            SqlCommand command = new SqlCommand("UPDATE UserInfo SET PhotoURL='"+photoURL+"' WHERE UserID=" + UserID, con);
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

        public List<String> getUserBanks(int UserID)
        {

            String list = "";
            List<String> bList = new List<string>();
            connection();
            SqlCommand command = new SqlCommand("SELECT BankIDs FROM UserInfo WHERE UserID=" + UserID, con);
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
                while (reader.Read())
                {

                    list = reader["BankIDs"].ToString();

                }
            }
            if (list != null)
            {
                char[] tempList = list.ToArray();
                for (int i = 0; i < tempList.Length; i++)
                {
                    if (tempList[i].Equals(',')){
                        continue;
                    } else
                    {
                         command = new SqlCommand("SELECT BankName FROM Banks WHERE BankID=" + tempList[i], con);
                        
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                bList.Add(reader["BankName"].ToString());

                            }
                        }
                    }
                }

                return bList;



            }
            else
            {
                return null;
            }




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

        public Boolean addBank(int UserID, int BankID)
        {
            Boolean r = false;

            String oldList = "";
            connection();
            SqlCommand command = new SqlCommand("SELECT BankIDs FROM UserInfo WHERE UserID=" + UserID, con);
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
                while (reader.Read())
                {

                    oldList = reader["BankIDs"].ToString();
                }
            }
            String newList = oldList + "," + BankID;

            command = new SqlCommand("UPDATE UserInfo SET BankIDs='"+newList+"' WHERE UserID="+UserID, con);

            int rInt = command.ExecuteNonQuery();

            if (rInt == 1)
                r = true;
                    return r;
        }
    }
}
