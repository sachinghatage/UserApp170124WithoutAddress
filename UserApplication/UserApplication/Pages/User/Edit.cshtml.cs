using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserApplication.Model;

namespace UserApplication.Pages.User
{
    public class EditModel : PageModel
    {
        private readonly IConfiguration configuration;
        [BindProperty]
        public Users User { get; set; } = new Users();

        public EditModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void OnGet()
        {
            int id =Convert.ToInt32( Request.Query["Id"]);
            DataAccessLayer dal = new DataAccessLayer();
            User = dal.GetUser(id,configuration);
        }

        public void OnPost(int id) {
            //Console.WriteLine($"Received Id: {id}");
           /* User.Name = Request.Form["name"];
            User.Email = Request.Form["Email"];
            User.Phone =Convert.ToInt32( Request.Form["Phone"]);
            string userGender = Request.Form["Gender"];
            if (Enum.TryParse(userGender, out Gender genderEnum))
            {
                User.UserGender = genderEnum;
            }

            User.City = Request.Form["City"];
            User.State = Request.Form["State"];
            User.Country = Request.Form["Country"];
            User.Street = Request.Form["Street"];
            User.PostalCode = Request.Form["PostalCode"];
*/


            DataAccessLayer dal =new DataAccessLayer();
            dal.UpdateUser(id, User, configuration);

            Response.Redirect("/User/list");

        }
    }
}
