using System.Threading.Tasks;

using fixit.Models;
using Microsoft.EntityFrameworkCore;

namespace fixit.Data
{
    public class JobRepository : IRepository<Job>
    {
        private readonly DataContext _context;
        public JobRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteData(Job job)
        {
            _context.Job.Remove(job);
            await _context.SaveChangesAsync();
            return true;

        }
        public async Task<Job> GetDataByEmail(string email)
        {
            return null;
        }


        public async Task<System.Collections.Generic.List<Job>> GetData()
        {
            var data = await _context.Job
             .Include(e => e.User)
             .Include(e => e.Technician).ThenInclude(x => x.User)
             .ToListAsync();
            return data;
        }

        public async Task<Job> GetDataById(int id)
        {
            return await _context.Job.FirstOrDefaultAsync(x => x.JobId == id);

        }

        public async Task<Job> InsertData(Job job)
        {
            _context.Job.Add(job);
            await _context.SaveChangesAsync();
            return job;

        }
        public async Task<Job> UpdateData(Job job)
        {
            _context.Update(job).Property(x => x.JobId).IsModified = false;
            await _context.SaveChangesAsync();
            return job;
        }
    }

}
