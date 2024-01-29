using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserApplication.Model;

namespace UserApplication.Pages.User
{
    public class EditModel : PageModel
    {
        private readonly IConfiguration configuration;
        public Users user = new Users();
       /* public Address address = new Address();*/
        public EditModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void OnGet()
        {
            int id =Convert.ToInt32( Request.Query["Id"]);
            DataAccessLayer dal = new DataAccessLayer();
            user=dal.GetUser(id,configuration);
        }

        public void OnPost(int id) {
            Console.WriteLine($"Received Id: {id}");
            user.Name = Request.Form["name"];
            user.Email = Request.Form["Email"];
            user.Phone =Convert.ToInt32( Request.Form["Phone"]);
            string userGender = Request.Form["Gender"];
            if (Enum.TryParse(userGender, out Gender genderEnum))
            {
                user.UserGender = genderEnum;
            }





            DataAccessLayer dal =new DataAccessLayer();
            dal.UpdateUser(id,user,configuration);

            Response.Redirect("/User/list");

        }
    }
}
