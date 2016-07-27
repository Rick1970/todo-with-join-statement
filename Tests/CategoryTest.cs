using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ToDoList
{
  public class CategoryTest : IDisposable
  {
    public  CategoryTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=todo_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void T1_CategoriesEmptyAtFirst()
    {
      // Arrange , Act
      int result = Category.GetAll().Count;

      // Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void T2_Equal_ReturnsTrueForSameName()
    {
      // Arrange, act
      Category firstCategory = new Category("Household chores");
      Category secondCategory = new Category("Household chores");

      // Assert
      Assert.Equal(firstCategory, secondCategory);
    }

    [Fact]
    public void T3_Save_SavesCategoryToDatabase()
    {
      // Arrange
      Category testCategory = new Category("Household chores");
      testCategory.Save();

      // Act
      List<Category> result = Category.GetAll();
      List<Category> testList = new List<Category>{testCategory};

      // Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void T4_Save_AssignsIdToCagetoryObject()
    {
      //Arrange

      Category testCategory = new Category("Household chores");
      testCategory.Save();

      //Act
      Category savedCategory = Category.GetAll()[0];

      int result = savedCategory.GetId();
      int testId = testCategory.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void T5_Find_FindsCategoryInDatabase()
    {
      //Arrange
      Category testCategory = new Category("Household chores");
      testCategory.Save();

      //Act
      Category foundCategory = Category.Find(testCategory.GetId());

      //Assert
      Assert.Equal(testCategory, foundCategory);
    }

    [Fact]
    public void T6_GetTasks_RetrievesAllTasksWithCategory()
    {
      Category testCategory = new Category("Household chores");
      testCategory.Save();

      Task firstTask = new Task("Mow the lawn", testCategory.GetId());
      firstTask.Save();
      Task secondTask = new Task("Do the dishes", testCategory.GetId());
      secondTask.Save();


      List<Task> testTaskList = new List<Task> {firstTask, secondTask};
      List<Task> resultTaskList = testCategory.GetTasks();

      Assert.Equal(testTaskList, resultTaskList);
    }

    [Fact]
    public void T7_Update_UpdatesCategoryInDatabase()
    {
      //Arrange
      string name = "Home Chores";
      Category testCategory = new Category(name);
      testCategory.Save();
      string newName = "Work Chores";

      //Act
      testCategory.Update(newName);

      string result = testCategory.GetName();

      //Assert
      Assert.Equal(newName, result);
    }

    public void Dispose()
    {
      Task.DeleteAll();
      Category.DeleteAll();
    }
  }
}
