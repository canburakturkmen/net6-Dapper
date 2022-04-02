using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Phone
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string PhoneNumber { get; set; }

    }
}
