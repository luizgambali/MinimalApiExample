namespace MinimalApi.Interfaces
{
    public interface IStudentRepository
    {
        Student? GetById(Guid id);
        IEnumerable<Student> GetAll();
        bool AddNew(Student student);
        bool Update(Student student);
        bool Delete(Student student);
        
    }
}
