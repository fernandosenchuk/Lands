namespace Lands.Helpers
{
    using Lands.Domain;
    using Lands.Models;

    public static class Converter
    {
        public static UserLocal ToUserLocal(User user)
        {
            if (user == null)
                return null;

            return new UserLocal()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                ImagePath = user.ImagePath,
                LastName = user.LastName,
                Telephone = user.Telephone,
                UserId = user.UserId,
                UserTypeId = user.UserTypeId
            };
        }
    }
}
