using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(bool Success, string Message)> RegisterUser(
        string email,
        string numeComplet,
        string parola)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        if (existingUser != null)
        {
            return (false, "Utilizatorul cu acest email există deja.");
        }

        var user = new User
        {
            Email = email,
            NumeComplet = numeComplet,
            Parola = parola
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return (true, "Cont creat cu succes!");
    }

    public async Task<(bool Success, string Message)> LoginUser(
         string email,
         string parola)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
        {
            return (false, "Acest cont nu există.");
        }

        if (user.Parola != parola)
        {
            return (false, "Parola este incorectă.");
        }

        return (true, "V-ați logat cu succes.");
    }
}