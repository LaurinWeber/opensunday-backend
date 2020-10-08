using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Location
{

[Key]
public int Id { get; set; }
public string Name { get; set; }

//Alex B - We need Creator for the post method in the LocationsController, GOT TO CHANGE IT LATER WITH FK_User !!!!!!!!!!!!
public string Creator { get; set; }

[Column(TypeName = "decimal(8,6)")]
 public decimal Lattitude {get; set; }

 [Column(TypeName = "decimal(9,6)")]
public decimal Longitude {get;set;}
public string Address {get; set; }
public string Telephone {get;set;} 
public string OpeningTime {get;set;}
public string ClosingTime {get;set;}
public int FK_Likes  {get;set;}
public int FK_User  {get;set;}
public int FK_Category {get; set;}
public int FK_City {get;set;} 


}