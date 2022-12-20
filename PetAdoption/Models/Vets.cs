using System.ComponentModel.DataAnnotations;
namespace PetAdoption.Models
{
public class Vets 
{    
    [Key]
    public Int64 id{ get; set;}
    public string? adress{ get; set;}
    public string? number{ get; set;}
    public string? name{ get; set;}
    public string?  localisation { get; set; }

}
}