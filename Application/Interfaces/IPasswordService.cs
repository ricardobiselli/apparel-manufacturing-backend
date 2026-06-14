namespace Application.Interfaces;

public interface IPasswordService
{
    // Receives a plain text password
    // Returns a secure hash
    string HashPassword(string password);

    // Receives:
    // - password entered by the user
    // - hash stored in the database
    //
    // Returns true if they match
    bool VerifyPassword(string password, string passwordHash);
}

