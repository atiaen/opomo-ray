public class Project
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreatedDate { get; set; }
}
/*
Table projects {
  id uuid [primary key]
  title varchar
  created_at varchar 
  start_date varchar
  end_date varchar
}

Table tasks {
  id uuid [primary key]
  name varchar
  description varchar
  break_type varchar
  is_completed boolean
  start_date varchar
  end_date varchar
  created_date varchar
  project_id nullable [ref: > projects.id]
}
*/

public class Tasks
{
    public Guid TaskId { get; set; }
    public string TaskName { get; set; } = "";
    public string TaskDesc { get; set; } = "";
    public string BreakType { get; set; } = "";
    public bool IsCompleted { get; set; } = false;
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public Guid? ProjectId { get; set; } = null;
}