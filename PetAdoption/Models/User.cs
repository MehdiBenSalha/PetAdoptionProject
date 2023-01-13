using PetAdoption_dotnet.Models;
using System.ComponentModel.DataAnnotations;

namespace PetAdoption.Models
{
    public class User
    {
        //public User(string adress,string mail,string number,int name, string localisation){  this.adress=adress; this.mail=mail; this.number=number; this.name=name; this.localisation=localisation;}
        
        [Key]   //mail
        public string userId { get; set; }
        public string? adress { get; set; }
        public string? name { get; set; }
        public string? number { get; set; }

        //Relationship
        public List<Pet> pets { get; set; }       
        public Vet vett { get; set; }

        public CentreAdressage centreAdressagee { get; set; }

    }
}

