using App.Core.DataModels;

namespace App.Entities.Settings
{
    public class Setting : EntityBase
    {
        public int Id { get; set; }

        public string Menu { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Skype { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Website { get; set; }
        public string Logo { get; set; }
    }
}
