﻿namespace Mix.Database.Entities.Account
{
    public partial class AspNetUserClaims
    {
        public int Id { get; set; }
        public string MixUserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUsers MixUser { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}