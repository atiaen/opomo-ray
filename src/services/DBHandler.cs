using System.Data.SQLite;

public class DBHandler
{
    SQLiteConnection sqlite_conn;
    public DBHandler()
    {
        sqlite_conn = CreateConnection();
        CreateInitalTables(sqlite_conn);
    }

    SQLiteConnection CreateConnection()
    {

        SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=opomo.db;Version=3;New=True;Compress=True;");
        try
        {
            sqlite_conn.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return sqlite_conn;
    }

    void CreateInitalTables(SQLiteConnection conn)
    {

        SQLiteCommand sqlite_cmd;
        string Createsql = "CREATE TABLE IF NOT EXISTS SampleTable (Col1 VARCHAR(20), Col2 INT)";
        string Createsql1 = "CREATE TABLE IF NOT EXISTS SampleTable1 (Col1 VARCHAR(20), Col2 INT)";
        sqlite_cmd = conn.CreateCommand();
        sqlite_cmd.CommandText = Createsql;
        sqlite_cmd.ExecuteNonQuery();
        sqlite_cmd.CommandText = Createsql1;
        sqlite_cmd.ExecuteNonQuery();

    }
}