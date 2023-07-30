namespace Microservices.PhoneBook.Dtos
{
    public static class ErrorMessages
    {
        public static class Contact
        {
            public static string EmptyId = "Id cannot be left empty";
            public static string NegativeId = "Id cannot have negative value";
        }
    }
}
