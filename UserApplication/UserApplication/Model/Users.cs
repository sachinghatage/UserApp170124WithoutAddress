using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UserApplication.Model
{
    public class Users
    {
        public long Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }    //? so that property should not be null  (string?)
       
        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }   //? so that property should not be null

        [Required(ErrorMessage ="phone no is required")]
        public int Phone { get; set; }

     
        [Required(ErrorMessage ="Gender is required")]
        public Gender UserGender { get; set; } = Gender.Unknown;

        public string City {  get; set; }
        public string State {  get; set; }
        public string Country { get; set; }
        public string Street {  get; set; }
        public string PostalCode {  get; set; }

      
    }

   
    public enum Gender
    {
        Male,Female,Unknown
    }
}
