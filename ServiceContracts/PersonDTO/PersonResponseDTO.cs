using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts.PersonDTO
{
    /// <summary>
    /// DTO Class that is used as a return type for most of PersonService methods
    /// </summary>
    public class PersonResponseDTO
    {
        public Guid PersonId { get; set; }

        public string? PersonName { get; set; }

        public string? Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public Guid? CountryId { get; set; }

        public string? CountryName { get; set; }

        public string? Address { get; set; }

        public bool? ReceiveNewsLetter { get; set; }

        public double? Age{ get; set; }

        /// <summary>
        /// Compares the current object with the parameter object
        /// </summary>
        /// <param name="obj">The PersonResponse Object to compare</param>
        /// <returns> True of False, indicating whether all person details are matched with the specified parameter object</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(PersonResponseDTO)) 
            {
                return false;
            }

            PersonResponseDTO typeCastedObject = (PersonResponseDTO)obj;

            return PersonId == typeCastedObject.PersonId
                && PersonName == typeCastedObject.PersonName
                && Email == typeCastedObject.Email
                && DateOfBirth == typeCastedObject.DateOfBirth
                && Gender == typeCastedObject.Gender
                && CountryId == typeCastedObject.CountryId
                && CountryName == typeCastedObject.CountryName
                && Address == typeCastedObject.Address
                && ReceiveNewsLetter == typeCastedObject.ReceiveNewsLetter;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"Person ID: {PersonId}, " +
                $"Person name: {PersonName}, " +
                $"Email: {Email}," +
                $" Date of birth: {DateOfBirth?.ToString("dd MMM yyyy")}, " +
                $"Gender: {Gender}, " +
                $"Country ID: {CountryId}," +
                $"Country name: {CountryName}," +
                $"Address: {Address}," +
                $"Receive News Letter: {ReceiveNewsLetter}";
        }

        public PersonUpdateDTO ToPersonUpdateDTO()
        {
            return new PersonUpdateDTO()
            {
                PersonId = PersonId,
                PersonName = PersonName,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Gender = (GenderOptions)Enum.Parse(typeof(GenderOptions), Gender, true),
                CountryId = CountryId,
                Address = Address,
                ReceiveNewsLetter = ReceiveNewsLetter,
            };
        }
    }

    public static class PersonExtensions 
    {
        /// <summary>
        /// An extension method to convert an object of Person class into PersonResponseDTO class
        /// </summary>
        /// <param name="person">The Person object to be converted</param>
        /// <returns>Returns the converted PersonResponseDTO object</returns>
        public static PersonResponseDTO ToPersonResponseDTO(this Person person)
        {
            return new PersonResponseDTO()
            {
                PersonId = person.PersonId,
                PersonName = person.PersonName,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                Gender = person.Gender,
                CountryId = person.CountryId,
                Address = person.Address,
                ReceiveNewsLetter = person.ReceiveNewsLetter,
                Age = (person.DateOfBirth != null) 
                ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25) 
                : null
            };
        }
    }
}
