using System.ComponentModel.DataAnnotations;
namespace PetAdoption_dotnet.Models
{
public class Vets
{    
    
    //public Vets(string adress,string mail,string number,int name, string localisation){  this.adress=adress; this.mail=mail; this.number=number; this.name=name; this.localisation=localisation;}
    [Key]
    public int id{ get; set;}
    public string? adress{ get; set;}
    public string? mail{ get; set;}
    public string? number{ get; set;}
    public int? name{ get; set;}
    public string?  localisation { get; set; }

}
}