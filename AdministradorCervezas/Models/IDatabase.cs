using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministradorCervezas.Models
{
    public interface IDatabaseCrud
    {
        bool Add();
        bool Edit();
        bool Delete();
    }
}
