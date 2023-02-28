using System.Threading.Tasks;

namespace Rich.BookMark.Data;

public interface IBookMarkDbSchemaMigrator
{
    Task MigrateAsync();
}
