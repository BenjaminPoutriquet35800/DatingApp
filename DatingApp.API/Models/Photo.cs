using System;

namespace DatingApp.API.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public String Url { get; set; }
        public String Description { get; set; }
        public DateTime DateAdded { get; set; }
        public Boolean IsMain { get; set; }
        public String PublicId { get; set; }
        public User User { get; set; }        
        public int UserId { get; set; }
    }
}