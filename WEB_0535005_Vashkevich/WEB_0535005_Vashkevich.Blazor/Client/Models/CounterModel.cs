using System.ComponentModel.DataAnnotations;

namespace WEB_0535005_Vashkevich.Blazor.Client.Models
{
    public class CounterModel
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "You should input value from 0 to 2147483647")]
        public int CustomCount { get; set; }
    }
}
