using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_0535005_Vashkevich.Blazor.Shared
{
    public class Album
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public TimeSpan Duration { get; set; }
        public string? Image { get; set; }

        [Display(Name = "Category")]
        public AlbumCategory Category { get; set; }
    }

    public class AlbumCategory
    {
        [Key]
        public int Id { get; set; }
        
        public string CategoryName { get; set; }
    }
}
