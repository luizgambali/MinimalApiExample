using Microsoft.EntityFrameworkCore;
using MinimalApi.Data.Context;
using MinimalApi.Interfaces;

namespace MinimalApi.Data.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Student> _dbSet;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Students;

        }
        public bool AddNew(Student student)
        {
            _dbSet.Add(student);
            _context.SaveChanges();
            return true;
        }

        public bool Update(Student student)
        {
            var result = _dbSet.Find(student.Id);
            
            if (result == null)
            {
                return false;
            }

            result.Name = student.Name;
            result.Email = student.Email;

            _dbSet.Update(result);
            _context.SaveChanges();

            return true;
        }

        public bool Delete(Student student)
        {
            _dbSet.Remove(student);
            _context.SaveChanges();

            return true;
        }

        public Student GetById(Guid id)
        {
            return _dbSet.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Student> GetAll()
        {
            return _dbSet.ToList();
        }
    }
}
