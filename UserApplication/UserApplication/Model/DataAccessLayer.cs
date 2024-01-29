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
                string query = "INSERT INTO users (Name, Email, Phone, FileContent,Gender) VALUES (@Name, @Email, @Phone, CONVERT(VARBINARY(MAX), @FileContent),@Gender);";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Phone", user.Phone);
                    command.Parameters.AddWithValue("@FileContent", user.FileContent); // Assuming user.FileContent is a byte array
                    command.Parameters.AddWithValue("@Gender", user.UserGender.ToString());//by default numbers are stored in db for enum so tostring() is used


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
                string query = "UPDATE users SET Name=@Name, Email=@Email, Phone=@Phone WHERE Id=@Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Phone", user.Phone);
                command.Parameters.AddWithValue("@Id", id); // Use the parameter 'id' here

                command.ExecuteNonQuery();
                connection.Close();
            }
        }


        /*  public void UpdateUser(Users user, Address address, IConfiguration configuration)
          {
              using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DBCS").ToString()))
              {
                  connection.Open();

                  // Update the users table
                  string updateUserQuery = "UPDATE users SET Name=@Name, Email=@Email, Phone=@Phone, Gender=@Gender WHERE Id=@UserId";
                  using (SqlCommand updateUserCommand = new SqlCommand(updateUserQuery, connection))
                  {
                      updateUserCommand.Parameters.AddWithValue("@UserId", user.Id);
                      updateUserCommand.Parameters.AddWithValue("@Name", user.Name);
                      updateUserCommand.Parameters.AddWithValue("@Email", user.Email);
                      updateUserCommand.Parameters.AddWithValue("@Phone", user.Phone);
                      updateUserCommand.Parameters.AddWithValue("@Gender", user.UserGender.ToString());

                      int userId = Convert.ToInt32(updateUserCommand.ExecuteScalar());

                      string updateAddressQuery = "UPDATE address SET Street=@Street, City=@City, State=@State, Zipcode=@Zipcode WHERE UserId=@UserId";
                      using (SqlCommand updateAddressCommand = new SqlCommand(updateAddressQuery, connection))
                      {
                          updateAddressCommand.Parameters.AddWithValue("@UserId", userId);
                          updateAddressCommand.Parameters.AddWithValue("@Street", address.Street);
                          updateAddressCommand.Parameters.AddWithValue("@City", address.City);
                          updateAddressCommand.Parameters.AddWithValue("@State", address.State);
                          updateAddressCommand.Parameters.AddWithValue("@Zipcode", address.ZipCode);

                          updateAddressCommand.ExecuteNonQuery();
                      }
                  }



              }
          }*/



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

                        }
                    }
                }
            }
            return user;
        }
     

    }
}
