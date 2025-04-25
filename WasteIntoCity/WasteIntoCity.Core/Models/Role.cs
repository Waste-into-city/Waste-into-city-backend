using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions.BadRequest400Exceptions;

namespace WasteIntoCity.Core.Models
{
    public class Role
    {
        public const int NAME_LENGTH_MIN = 1;

        public const int NAME_LENGTH_MAX = 45;

        private Role(RoleEnum id, string name)
        {
            Id = id;
            Name = name;
        }

        public RoleEnum Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public static Role Create(RoleEnum id, string name)
        {
            if (name.Length is < NAME_LENGTH_MIN or > NAME_LENGTH_MAX)
            {
                throw new InvalidLengthException(11, nameof(name), NAME_LENGTH_MIN, NAME_LENGTH_MAX);
            }

            return new Role(id, name);
        }
    }
}
