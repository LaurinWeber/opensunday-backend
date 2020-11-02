using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class LocationCityCat
{
[Key]
public int Id { get; set; }

public string Name { get; set; }
[Column(TypeName="decimal(8,6)")]
 public decimal Latitude {get; set; }

[Column(TypeName="decimal(9,6)")]
public decimal Longitude {get;set;}

public string Address {get; set; }
public string Telephone {get;set;} 
public string OpeningTime {get;set;}
public string ClosingTime {get;set;}


public string CategoryName {get; set;}
public int NPA {get;set;} 
public string CityName{get; set;}


}