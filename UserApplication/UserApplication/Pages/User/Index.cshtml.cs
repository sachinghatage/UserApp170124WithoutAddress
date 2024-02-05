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
        [BindProperty]
        public Users User { get; set; } = new Users();

        public IndexModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

/*
        [BindProperty]   //instead of using this property we can create only object,but to simplify we have added
        public Users user { get; set; } = new Users();*/

        

        public void OnGet()
        {
        }
// manually retrieving values using request.form
         /*public void OnPost()             //return type can be IActionresult but here void can be used,both will redirect default to same page
         {
             //for validation,if data not provided returns the same page
             if(!ModelState.IsValid)
             {
                 return;
             }

             try
             {

                 User.Name = Request.Form["Name"];
                 User.Email = Request.Form["Email"];
                 User.Phone = Convert.ToInt32(Request.Form["Phone"]);
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





                 //calling save method from data access layer
                 DataAccessLayer dal = new DataAccessLayer();
                 dal.Saveuser(User, configuration);

                 logger.LogInformation($"User {User.Name} successfully saved");
             }
             catch (Exception ex)
             {

                 logger.LogError(ex, "error occured while processing form");
             }

             Response.Redirect("/User/List");





         }*/

        //model binding(binding model values with asp frameworks help)
        public IActionResult OnPost()
        {
            // For validation, if data is not provided, return the same page
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"ModelState error: {error.ErrorMessage}");
                }
                return Page();
            }

            try
            {
                // Avoid using Request.Form; let model binding handle it
                
                
                /*User.Name = Request.Form["Name"];
                User.Email = Request.Form["Email"];
                User.Phone = Convert.ToInt32(Request.Form["Phone"]);
                string userGender = Request.Form["Gender"];
                if (Enum.TryParse(userGender, out Gender genderEnum))
                {
                    User.UserGender = genderEnum;
                }
                User.City = Request.Form["City"];
                User.State = Request.Form["State"];
                User.Country = Request.Form["Country"];
                User.Street = Request.Form["Street"];
                User.PostalCode = Request.Form["PostalCode"];*/
                

                // The model binding system has already populated the User property
                // with the values from the form fields.

                // calling save method from the data access layer
                DataAccessLayer dal = new DataAccessLayer();
                dal.Saveuser(User, configuration);

                logger.LogInformation($"User {User.Name} successfully saved");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while processing form");
                // Handle the exception accordingly, e.g., return an error page
                return RedirectToPage("/Error");
            }

            // Use RedirectToPage instead of Response.Redirect
            // Assuming your List page is named "List.cshtml"
            return RedirectToPage("/User/List");
        }



    }
}
