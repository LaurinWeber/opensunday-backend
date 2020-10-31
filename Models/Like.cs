    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


        public class Like
        {
        [Required]
        [ForeignKey("Location")]   
        public int FK_Location{ get; set; }


        [ForeignKey("User")]
        public string FK_User {get;set;}

        public int isLiked {get;set;}

        }
 