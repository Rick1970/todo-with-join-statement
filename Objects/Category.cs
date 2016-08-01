using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace ToDoList
{
  public class Category
  {
    private int _id;
    private string _name;

    //Set ID to zero by default, as it is set by SQL
    public Category(string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;
    }

    //Override Equals system method so the tests can be overridden (inside scope of this class)
    public override bool Equals(System.Object otherCategory)
    {
      if (!(otherCategory is Category))
      {
        return false;
      }
      else
      {
        //Declare and cast newCategory
        Category newCategory = (Category) otherCategory;
        //Make sure IDs match
        bool idEquality = (this.GetId() == newCategory.GetId());
        //Make sure names match
        bool nameEquality = (this.GetName() == newCategory.GetName());
        //Only return true if both match
        return (idEquality && nameEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }

    public static List<Category> GetAll()
    {
      List<Category> allCategories = new List<Category>{};

      //Open connection
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new  SqlCommand("SELECT * FROM categories;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      //SqlDataReader.Read() method returns boolean - true if more rows, false otherwise
      while(rdr.Read())
      {
        //Specific methods to get types from DB
        int categoryId = rdr.GetInt32(0);
        string categoryName = rdr.GetString(1);
        Category newCategory = new Category(categoryName, categoryId);
        allCategories.Add(newCategory);
      }

      //More explanation needed...
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allCategories;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO categories (name) OUTPUT INSERTED.id VALUES (@CategoryName);", conn);

      //Pass to SqlParameter - @ required
      SqlParameter nameParameter = new SqlParameter();
      //Manually assign properties - could also use as arguments in constructor
      //Dummy variable - Gets the name from the object - same as Name property above (in constructor) - column title
      nameParameter.ParameterName = "@CategoryName";
      nameParameter.Value = this.GetName();

      cmd.Parameters.Add(nameParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        //Making sure object in memory matches data in database (autoincremented ID)
        this._id = rdr.GetInt32(0);
      }
      //Close these if they exist  - if null occurs, there is likely another issue going on...
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM categories;", conn);
      //Use ExecuteNonQuery method when executing a Update/Delete command
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static Category Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM categories WHERE id = @CategoryId;", conn);
      SqlParameter categoryIdParameter = new SqlParameter();
      categoryIdParameter.ParameterName = "@CategoryId";
      categoryIdParameter.Value = id.ToString();
      cmd.Parameters.Add(categoryIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundCategoryId = 0;
      string foundCategoryDescription = null;

      while(rdr.Read())
      {
        foundCategoryId = rdr.GetInt32(0);
        foundCategoryDescription = rdr.GetString(1);
      }
      Category foundCategory = new Category(foundCategoryDescription, foundCategoryId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundCategory;
    }


    public void Update(string newName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE categories SET name = @NewName OUTPUT INSERTED.name WHERE id = @CategoryId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);


      SqlParameter categoryIdParameter = new SqlParameter();
      categoryIdParameter.ParameterName = "@CategoryId";
      categoryIdParameter.Value = this.GetId();
      cmd.Parameters.Add(categoryIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }
    }

    public void AddTask(Task newTask)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO categories_tasks(category_id,task_id) VALUES(@CategoryId,@TaskId);",conn);
      SqlParameter categoryIdParameter = new SqlParameter();
      categoryIdParameter.ParameterName = "@categoryId";
      categoryIdParameter.Value=this.GetId();
      cmd.Parameters.Add(categoryIdParameter);

      SqlParameter taskIdParameter = new SqlParameter();
      taskIdParameter.ParameterName="@TaskId";
      taskIdParameter.Value=newTask.GetId();
      cmd.Parameters.Add(taskIdParameter);

      cmd.ExecuteNonQuery();
      if(conn != null)
      {
        conn.Close();
      }
    }
    public List<Task> GetTasks()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT task_id FROM categories_tasks WHERE category_id = @CategoryId;", conn);
      SqlParameter categoryIdParameter = new SqlParameter();
      categoryIdParameter.ParameterName = "@CategoryId";
      categoryIdParameter.Value = this.GetId();
      cmd.Parameters.Add(categoryIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      List<int> taskIds = new List<int> {};
      while(rdr.Read())
      {
        int taskId = rdr.GetInt32(0);
        taskIds.Add(taskId);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      List<Task> tasks = new List<Task>{};
      foreach(int taskId in taskIds)
      {
          SqlCommand taskQuery = new SqlCommand("SELECT * FROM tasks WHERE id= @TaskId ORDER BY due_date;",conn);

          SqlParameter taskIdParameter= new SqlParameter();
          taskIdParameter.ParameterName="@TaskId";
          taskIdParameter.Value=taskId;
          taskQuery.Parameters.Add(taskIdParameter);

          SqlDataReader queryReader = taskQuery.ExecuteReader();
          while(queryReader.Read())
          {
            int thisTaskId=queryReader.GetInt32(0);
            string taskDescription=queryReader.GetString(1);
            DateTime taskDueDate = queryReader.GetDateTime(2);
            Task foundTask = new Task(taskDescription, taskDueDate, thisTaskId);
            tasks.Add(foundTask);
          }
          if(queryReader!= null)
          {
            queryReader.Close();
          }
        }
        if(conn != null)
        {
          conn.Close();
        }
        return tasks;


    }


    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM categories WHERE id = @CategoryId;DELETE FROM categories_tasks WHERE category_id = @CategoryId;", conn);

      SqlParameter categoryIdParameter = new SqlParameter();
      categoryIdParameter.ParameterName = "@CategoryId";
      categoryIdParameter.Value = this.GetId();

      cmd.Parameters.Add(categoryIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }



  }
}
