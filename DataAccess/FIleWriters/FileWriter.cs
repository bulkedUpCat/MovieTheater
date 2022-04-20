using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.FIleWriters
{
    public class FileWriter<T> where T : class
    {
        public bool WriteToFile(T entity, string type)
        {
            if (entity == null)
            {
                return false;
            }

            using (StreamWriter writer = new StreamWriter($"data{type}.txt", true))
            {
                writer.WriteLine(entity);
                return true;
            }
        }
    }
}
