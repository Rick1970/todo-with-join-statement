using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace ToDoList
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        List<Category> AllCategories = Category.GetAll();
        return View["index.cshtml", AllCategories];
      };
      Get["/tasks"] = _ => {
        List<Task> AllTasks = Task.GetAll();
        return View["tasks.cshtml", AllTasks];
      };
      Get["/categories"] = _ => {
        List<Category> AllCategories = Category.GetAll();
        return View["categories.cshtml", AllCategories];
      };
      Get["/categories/new"] = _ => {
        return View["categories_form.cshtml"];
      };
      Post["/categories/new"] = _ => {
        Category newCategory = new Category(Request.Form["category-name"]);
        newCategory.Save();
        return View["success.cshtml"];
      };
      Get["/tasks/new"] = _ => {
        return View["tasks_form.cshtml"];
      };
      Post["/tasks/new"] = _ => {
        Task newTask = new Task(Request.Form["task-description"],Request.Form["task-dueDate"]);
        newTask.Save();
        return View["success.cshtml"];
      };
      Post["/tasks/delete"] = _ => {
        Task.DeleteAll();
        return View["cleared.cshtml"];
      };
      Get["/categories/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var SelectedCategory = Category.Find(parameters.id);
        var CategoryTasks = SelectedCategory.GetTasks();
        model.Add("category", SelectedCategory);
        model.Add("tasks", CategoryTasks);
        return View["category.cshtml", model];
      };
      Get["category/edit/{id}"] = parameters => {
        Category SelectedCategory = Category.Find(parameters.id);
        return View["category_edit.cshtml", SelectedCategory];
      };
      Patch["category/edit/{id}"] = parameters => {
        Category SelectedCategory = Category.Find(parameters.id);
        SelectedCategory.Update(Request.Form["category-name"]);
        return View["success.cshtml"];
      };
      Get["category/delete/{id}"] = parameters => {
        Category SelectedCategory = Category.Find(parameters.id);
        return View["category_delete.cshtml", SelectedCategory];
      };
      Delete["category/delete/{id}"] = parameters => {
        Category SelectedCategory = Category.Find(parameters.id);
        SelectedCategory.Delete();
        return View["success.cshtml"];
      };

      Get["tasks/{id}"]=parameters=>{
        Dictionary<string, object> model = new Dictionary<string, object>();
        Task SelectedTask = Task.Find(parameters.id);
        List<Category> TaskCategories = SelectedTask.GetCategories();
        List<Category> AllCategories = Category.GetAll();
        model.Add("task",SelectedTask);
        model.Add("taskCategories",TaskCategories);
        model.Add("allCategories",AllCategories);
        return View["task.cshtml",model];
      };

      Get["categories/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Category selectedCategory = Category.Find(parameters.id);
        List<Task> categoryTasks = selectedCategory.GetTasks();
        List<Task> allTasks = Task.GetAll();
        model.Add("category", selectedCategory);
        model.Add("categoryTasks", categoryTasks);
        model.Add("allTasks", allTasks);
        return View["category.cshtml", model];
      };
      Post["task/add_category"] = _ =>{
        Category category= Category.Find(Request.Form["category-id"]);
        Task task = Task.Find(Request.Form["task-id"]);
        task.AddCategory(category);
        return View["success.cshtml"];
      };
      Post["category/add_task"] = _ => {
        Category category = Category.Find(Request.Form["category-id"]);
        Task task = Task.Find(Request.Form["task-id"]);
        category.AddTask(task);
        return View["success.cshtml"];
      };
    }
  }
}
