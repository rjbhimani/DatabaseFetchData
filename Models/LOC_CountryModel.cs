using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DatabaseFetchData.Models
{
    public class LOC_CountryModel
    {
        public int? CountryID { get; set; }

        [Required(ErrorMessage = "Please enter Country Name")]
        [DisplayName("Country Name")]
        [StringLength(10,MinimumLength = 3)]
        public string CountryName { get; set; }

        [Required(ErrorMessage = "Please enter Country Code")]
        [DisplayName("Country Code")]
        public string CountryCode { get; set; }

        /*[Required(ErrorMessage = "Please enter CreationDate")]*/
        public DateTime CreationDate { get; set; }

        /*[Required(ErrorMessage = "Please enter Modification")]*/
        public DateTime Modification { get; set; }


    }

    public class LOC_CountryDropDownModel
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
    }
}
