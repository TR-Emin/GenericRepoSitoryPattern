using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("owners")]
    public class Owner
    {
        [Column("OwnerId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name ise required")]
        [StringLength(60,ErrorMessage="Name can't be longer then 60 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address ise required")]
        [StringLength(60, ErrorMessage = "Address can't be longer then 100 characters")]
        public string Address { get; set; }

        public ICollection<Account>Accounts { get; set; }

    }

}
