using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
namespace RegistrationLoginApi.Data.DataModels
{
    [Table("app_users")]
    public class User
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("firstname")]
        public string FirstName { get; set; }
        [Column("lastname")]
        public string LastName { get; set; }
        [Column("username")]
        public string Username { get; set; }

        [Column("passwordhash")]
        [JsonIgnore]
        public string PasswordHash { get; set; }

        [Column("is_service_acct")]
        public bool IsServiceAccount {get;set;}
    }
}