using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace backend.Models
{
    public class Claim
    {
        [Key]
        public int ClaimId {get; set;}
        //Claimant FK
        public int UserId {get; set;}
        public User User {get; set;} = null!;
        public DateOnly ClaimDate {get; set;}
        //FoundItem FK
        public int FoundId {get; set;}
        public FoundItem FoundItem {get; set;} = null!;

    }
}