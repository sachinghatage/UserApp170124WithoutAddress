using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Metadata.Ecma335;
using UserApplication.Model;

namespace UserApplication.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<IndexModel> logger;

        public IndexModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }


        [BindProperty]   //instead of using this property we can create only object,but to simplify we have added
        public Users user { get; set; } = new Users();

        

        public void OnGet()
        {
        }

        public void OnPost()             //return type can be IActionresult but here void can be used,both will redirect default to same page
        {
            try
            {
              
                user.Name = Request.Form["Name"];
                user.Email = Request.Form["Email"];
                user.Phone = Convert.ToInt32(Request.Form["Phone"]);
                string userGender = Request.Form["Gender"];
                if (Enum.TryParse(userGender, out Gender genderEnum))
                {
                    user.UserGender = genderEnum;
                }



                IFormFile file = Request.Form.Files["FileContent"];

                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    user.FileContent = memoryStream.ToArray();
                }

                user.City = Request.Form["City"];
                user.State = Request.Form["State"];
                user.Country = Request.Form["Country"];
                user.Street = Request.Form["Street"];
                user.PostalCode = Request.Form["PostalCode"];




                //calling save method from data access layer
                DataAccessLayer dal = new DataAccessLayer();
                dal.Saveuser(user, configuration);

                logger.LogInformation($"User {user.Name} successfully saved");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "error occured while processing form");
            }

            Response.Redirect("/User/List");

        }


    }
}
