using System.Data;
using System.Data.SQLite;

public class DBHandler
{
    SQLiteConnection sqlite_conn;
    public DBHandler()
    {
        sqlite_conn = CreateConnection();
        CreateInitalTables();
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

    public void CreateInitalTables()
    {

        SQLiteCommand sqlite_cmd;
        string Createsql = @"
        CREATE TABLE IF NOT EXISTS projects(
            id VARCHAR PRIMARY KEY, 
            title VARCHAR,
            created_at varchar,
            start_date varchar,
            end_date varchar
        )
        ";
        string Createsql1 = @"
        CREATE TABLE IF NOT EXISTS tasks(
            id VARCHAR PRIMARY KEY,
            name varchar,
            description varchar,
            break_type varchar,
            is_completed boolean,
            start_date varchar,
            end_date varchar,
            created_date varchar,
            project_id varchar nullable
        )
        ";
        string alterTable = @"
            ALTER TABLE tasks
            ADD FOREIGN KEY (project_id) 
            REFERENCES projects (id);
        ";
        sqlite_cmd = sqlite_conn.CreateCommand();

        sqlite_cmd.CommandText = Createsql;
        sqlite_cmd.ExecuteNonQuery();

        sqlite_cmd.CommandText = Createsql1;
        sqlite_cmd.ExecuteNonQuery();

        sqlite_cmd.CommandText = alterTable;
        sqlite_cmd.ExecuteNonQuery();

    }

    public List<Project> ReadFromProjects()
    {
        List<Project> projects = new List<Project>();
        SQLiteDataReader sqlite_datareader;
        SQLiteCommand sqlite_cmd;
        sqlite_cmd = sqlite_conn.CreateCommand();
        sqlite_cmd.CommandText = "SELECT * FROM projects";

        sqlite_datareader = sqlite_cmd.ExecuteReader();
        while (sqlite_datareader.Read())
        {
            Project project = new Project
            {
                Id = sqlite_datareader.GetGuid(0),
                Title = sqlite_datareader.GetString(1),
                StartDate = sqlite_datareader.GetDateTime(2),
                EndDate = sqlite_datareader.GetDateTime(3),
                CreatedDate = sqlite_datareader.GetDateTime(4)
            };

            projects.Add(project);
        }
        sqlite_conn.Close();


        return projects;
    }

    public List<Tasks> ReadFromTasks()
    {
        List<Tasks> tasks = new List<Tasks>();
        SQLiteDataReader sqlite_datareader;
        SQLiteCommand sqlite_cmd;
        sqlite_cmd = sqlite_conn.CreateCommand();
        sqlite_cmd.CommandText = "SELECT * FROM tasks";

        sqlite_datareader = sqlite_cmd.ExecuteReader();
        while (sqlite_datareader.Read())
        {
            Tasks task = new Tasks()
            {
                TaskId = sqlite_datareader.GetGuid(0),
                TaskName = sqlite_datareader.GetString(1),
                TaskDesc = sqlite_datareader.GetString(2),
                BreakType = sqlite_datareader.GetString(3),
                StartDate = sqlite_datareader.GetDateTime(4),
                EndDate = sqlite_datareader.GetDateTime(5),
                CreatedDate = sqlite_datareader.GetDateTime(6),
                ProjectId = sqlite_datareader.GetGuid(7),
            };

            tasks.Add(task);
        }
        sqlite_conn.Close();


        return tasks;
    }

    public int CreateProject(Project project)
    {
        SQLiteCommand sqlite_cmd;
        sqlite_cmd = sqlite_conn.CreateCommand();

        sqlite_cmd.CommandText =
       string.Format(@"
        INSERT INTO project VALUES 
         ({0},{1},{2},{3},{4});"
        ,
        project.Id.ToString(),
        project.Title,
        project.StartDate.ToLongDateString(),
        project.EndDate.ToLongDateString(),
        project.CreatedDate.ToLongDateString());

        var res = sqlite_cmd.ExecuteNonQuery();
        return res;
    }

    public int CreateTasks(Tasks tasks)
    {
        SQLiteCommand sqlite_cmd;
        sqlite_cmd = sqlite_conn.CreateCommand();

        sqlite_cmd.CommandText =
       string.Format(@"
        INSERT INTO tasks VALUES 
         ({0},{1},{2},{3},{4},{5},{6},{7});"
        , tasks.TaskId.ToString(),
        tasks.TaskName,
        tasks.TaskDesc,
        tasks.BreakType,
        tasks.StartDate.ToLongDateString(),
        tasks.EndDate.ToLongDateString(),
        tasks.CreatedDate.ToLongDateString(),
        tasks.ProjectId.ToString()
        );

        var res = sqlite_cmd.ExecuteNonQuery();
        return res;
    }

    public Project GetProject(string id)
    {

        Project p = new Project();
        SQLiteDataReader sqlite_datareader;
        SQLiteCommand sqlite_cmd;
        sqlite_cmd = sqlite_conn.CreateCommand();
        sqlite_cmd.CommandText = string.Format("SELECT * FROM projects WHERE id = {0}", id);

        sqlite_datareader = sqlite_cmd.ExecuteReader();
        while (sqlite_datareader.Read())
        {

            p.Id = sqlite_datareader.GetGuid(0);
            p.Title = sqlite_datareader.GetString(1);
            p.StartDate = sqlite_datareader.GetDateTime(2);
            p.EndDate = sqlite_datareader.GetDateTime(3);
            p.CreatedDate = sqlite_datareader.GetDateTime(4);

        }
        sqlite_conn.Close();

        return p;
    }
}