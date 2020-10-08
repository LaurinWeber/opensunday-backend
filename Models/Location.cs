using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Location
{

[Key]
public int Id { get; set; }
[Required]
public string Name { get; set; }

//Alex B - We need Creator for the post method in the LocationsController, GOT TO CHANGE IT LATER WITH FK_User !!!!!!!!!!!!
public string Creator { get; set; }

[Required]
[Column(TypeName="decimal(8,6)")]
 public decimal Latitude {get; set; }

[Required]
[Column(TypeName="decimal(9,6)")]
public decimal Longitude {get;set;}

[Required]
public string Address {get; set; }
public string Telephone {get;set;} 
[Required]
public string OpeningTime {get;set;}
[Required]
public string ClosingTime {get;set;}


[Required]
[ForeignKey("Like")]
public int FK_Like {get;set;}


[Required]
[ForeignKey("User")]
public int FK_User  {get;set;}

[Required]
[ForeignKey("Category")]
public int FK_Category {get; set;}


[Required]
[ForeignKey("City")]
public int FK_City {get;set;} 


}