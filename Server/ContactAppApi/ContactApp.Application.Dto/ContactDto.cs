namespace ContactApp.Application.Dto
{
    public class ContactDto
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; } = string.Empty;
        public required string LastName { get; set; } = string.Empty;
        public required string Email { get; set; } = string.Empty;

    }
}
