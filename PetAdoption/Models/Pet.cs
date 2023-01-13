using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetAdoption.Models
{
    public class Pet
    {
        //public Pets(string adress,string mail,string number,int name, string localisation){  this.adress=adress; this.mail=mail; this.number=number; this.name=name; this.localisation=localisation;}
        [Key]
        public int petId { get; set; }
        public string? userAdress { get; set; }
        public string?  mail { get; set; }
        public string? number { get; set; }
        public string? type { get; set; }
        public string? race { get; set; }
        public string? localisation { get; set; }

        //Relationship
        public string userId { get; set; }

        [ForeignKey("userId")]
        public User user { get; set; }
    }
}
