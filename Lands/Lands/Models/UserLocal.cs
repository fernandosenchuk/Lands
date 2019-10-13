namespace Lands.Models
{
    using SQLite.Net.Attributes;
    using Xamarin.Forms;

    public class UserLocal
    {
        [PrimaryKey]
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public string ImagePath { get; set; }

        public int? UserTypeId { get; set; }

        public string Password { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(ImagePath))
                {
                    return "noimage";
                }

                var apiSecurity = Application.Current.Resources["APISecurity"].ToString();

                return string.Format(
                    "{0}/{1}",
                    apiSecurity,
                    ImagePath.Substring(1));

                //"http://landsapi1.azurewebsites.net/{0}",
            }
        }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        public override int GetHashCode()
        {
            return UserId;
        }
    }
}
