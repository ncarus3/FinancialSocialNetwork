namespace FinancialSocialNetwork.Models
{
    public class UserModel
    {
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String username { get; set; }
        public int userID { get; set; }
        public String email { get; set; }

        public String country { get; set; }

        public String phoneNumber { get; set; }
        public String photoURL { get; set; }
        public String bio { get; set; }
        public String bankIDs{ get; set; }

        public List<String> banksList { get; set; }
    }
}
