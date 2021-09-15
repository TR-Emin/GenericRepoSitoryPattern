using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("accounts")]
    public class Account
    {
        [Column("AccountId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "DateCreated ise required")]
        public DateTime DateCreated { get; set; }

        [Required(ErrorMessage = "AccountType ise required")]
        public string AccountType { get; set; }

        [ForeignKey(nameof(Owner))]
        public Guid OwnerId { get; set; }

        public Owner Owner { get; set; }

    }
}
