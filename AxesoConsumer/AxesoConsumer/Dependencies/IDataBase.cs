using System;
using System.Collections.Generic;
using System.Text;

namespace AxesoConsumer.Dependencies
{
    public interface IDataBase
    {
        SQLite.SQLiteConnection GetConnection();
    }
}
