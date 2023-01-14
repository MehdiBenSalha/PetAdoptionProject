using PetAdoption_dotnet.Models;
using System.ComponentModel.DataAnnotations;

namespace PetAdoption.Models.DbModels
{
    public class Userr
    {
        //public User(string adress,string mail,string number,int name, string localisation){  this.adress=adress; this.mail=mail; this.number=number; this.name=name; this.localisation=localisation;}
        
        [Key]   //mail
        public string userrId { get; set; }
        public string? adress { get; set; }
        public string? name { get; set; }
        public string? number { get; set; }

        //Relationship
        public IEnumerable<Pet> pets { get; set; }       
        public IEnumerable<Vet> vett { get; set; }

        public IEnumerable<CentreAdressage> centreAdressagee { get; set; }

    }
}
