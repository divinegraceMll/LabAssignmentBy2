using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using Student.Web.Api.Models;

namespace Student.Web.Api.Data
{
    public class PupilRepository : IPupilRepository
    {
        private readonly StudentDataContext _studentContex;


        public PupilRepository(StudentDataContext studentContext)
        {
            _studentContex = studentContext;

        }
        public void Add(Pupil newT)
        {
            _studentContex.Add(newT);
        }

        public void Delete(Pupil item)
        {
            _studentContex.Remove(item);
        }

        public async Task<bool> SaveAllChangesAsync()
        {
            return await _studentContex.SaveChangesAsync() > 0;
        }

        public async void Update<K>(K id, Pupil input)
        {
            // Get the pupil
            var thePupil = await _studentContex.Students.FindAsync(id);
            thePupil.LastName = input.LastName;
            thePupil.FirsName = input.FirsName;
            thePupil.MiddleName = input.MiddleName;
        }

        public async Task<List<Pupil>> GetAllAsync()
        {
            return await _studentContex.Students.ToListAsync();
        }

        public async Task<Pupil?> GetById(string id)
        {
            return await _studentContex.Students.FirstOrDefaultAsync(x => x.StudentId == id);
        }

    }
}