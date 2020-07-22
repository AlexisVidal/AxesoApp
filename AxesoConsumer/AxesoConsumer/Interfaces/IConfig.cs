using System;
using System.Collections.Generic;
using System.Text;

namespace AxesoConsumer.Interfaces
{
    using SQLite.Net.Interop;

    public interface IConfig
    {
        string DirectoryDB { get; }

        ISQLitePlatform Platform { get; }
    }
}
