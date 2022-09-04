
public class Student
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public Student(Guid id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public bool Validate()
    {
        if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email))
            return false;

        return true;
    }
}



