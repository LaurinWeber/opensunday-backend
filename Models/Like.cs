using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Like
{


[Required]
[ForeignKey("Location")]   
public long FK_Location{ get; set; }


[Required]
[ForeignKey("User")]
public int FK_User {get;set;}

public int Likes{get;set;}

}