using System.Data.SqlClient;


namespace UserApplication.Model
{
    public class DataAccessLayer
    {
        public void Saveuser(Users user, IConfiguration configuration)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DBCS").ToString()))
            {
                connection.Open();
                string query = "INSERT INTO users (Name, Email, Phone,Gender,City,State,Country,Street,PostalCode) VALUES (@Name, @Email, @Phone,@Gender,@City,@State,@Country,@Street,@PostalCode);";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Phone", user.Phone);
                   
                    command.Parameters.AddWithValue("@Gender", user.UserGender.ToString());//by default numbers are stored in db for enum so tostring() is used
                    command.Parameters.AddWithValue("@City",user.City);
                    command.Parameters.AddWithValue("@State", user.State);
                    command.Parameters.AddWithValue("@Country", user.Country);
                    command.Parameters.AddWithValue("@Street", user.Street);
                    command.Parameters.AddWithValue("@PostalCode", user.PostalCode);


                    command.ExecuteNonQuery();
                   
                }
            }
        }

        public List<Users> GetUsers(IConfiguration configuration)
        {
            List<Users> users = new List<Users>();
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DBCS")))
            {
                connection.Open();
               
                string query = "select * from users";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Users user = new Users();
                            user.Id =Convert.ToInt32( reader["Id"]);
                            user.Name = Convert.ToString(reader["Name"]);
                            user.Email = Convert.ToString(reader["Email"]);
                         
                            string userGender = Convert.ToString(reader["Gender"]);

                            if (Enum.TryParse(userGender, out Gender enumgender))
                            {
                                user.UserGender = enumgender;
                            }

                            user.Country =Convert.ToString( reader["Country"]);


                            users.Add(user);
                        }
                    }
                }
            }
            return users;
        }

        public void deleteUser(int id, IConfiguration configuration)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DBCS")))
            {
                connection.Open();
                string query = "delete from users where id = '" + id + "'";

                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void UpdateUser(int id, Users user, IConfiguration configuration)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DBCS")))
            {
                connection.Open();
                string query = "UPDATE users SET Name=@Name, Email=@Email, Phone=@Phone,City=@City,State=@State,Country=@Country,Street=@Street,PostalCode=@PostalCode,Gender=@Gender WHERE Id=@Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Phone", user.Phone);
                command.Parameters.AddWithValue("@Id", id); // Use the parameter 'id' here
                command.Parameters.AddWithValue("@City",user.City);
                command.Parameters.AddWithValue("@State", user.State);
                command.Parameters.AddWithValue("@Country", user.Country);
                command.Parameters.AddWithValue("@Street", user.Street);
                command.Parameters.AddWithValue("@PostalCode", user.PostalCode);
                command.Parameters.AddWithValue("@Gender",user.UserGender.ToString());


                command.ExecuteNonQuery();
                connection.Close();
            }
        }


        



        public Users GetUser(int id, IConfiguration configuration)
        {
            Users user = new Users();
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DBCS")))
            {
                connection.Open();
                string query = "select * from users where Id='" + id + "'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user.Id = Convert.ToInt32(reader["id"]);
                            user.Name = Convert.ToString(reader["Name"]);
                            user.Email = Convert.ToString(reader["Email"]);
                            user.Phone = Convert.ToInt32(reader["Phone"]);
                            string userGender =Convert.ToString( reader["Gender"]);
                            if(Enum.TryParse(userGender,out Gender gender))
                            {
                                user.UserGender= gender;
                            }
                            user.City =Convert.ToString( reader["City"]);
                            user.State = Convert.ToString(reader["State"]);
                            user.Country = Convert.ToString(reader["Country"]);
                            user.Street = Convert.ToString(reader["Street"]);
                            user.PostalCode = Convert.ToString(reader["PostalCode"]);


                        }
                    }
                }
            }
            return user;
        }
     

    }
}
