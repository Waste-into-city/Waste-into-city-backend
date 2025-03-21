using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions;

namespace WasteIntoCity.Core.Models
{
    public class Role
    {
        public const int NAME_LENGTH_MIN = 1;

        public const int NAME_LENGTH_MAX = 45;

        private Role(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public static Role Create(int id, string name)
        {
            if (!Enum.IsDefined(typeof(RoleEnum), id))
            {
                throw new InvalidValueFormatException(nameof(Role),
                    $"The value should be an enum ({ToStringAllValidValuesThroughComma()})");
            }

            return new Role(id, name);
        }

        private static string ToStringAllValidValuesThroughComma()
        {
            var roleTypes = Enum.GetValues<RoleEnum>();

            return string.Join(", ", roleTypes.Select(v => $"{(int)v} = {v}"));
        }
    }
}
