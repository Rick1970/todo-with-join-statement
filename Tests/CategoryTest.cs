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

    [Fact]
    public void T8_Delete_DeletesCategoryFromDatabase()
    {
      //Arrange
      string name1 = "Home stuff";
      Category testCategory1 = new Category(name1);
      testCategory1.Save();

      string name2 = "Work stuff";
      Category testCategory2 = new Category(name2);
      testCategory2.Save();

      //Act
      testCategory1.Delete();
      List<Category> resultCategories = Category.GetAll();
      List<Category> testCategoryList = new List<Category> {testCategory2};

      //Assert
      Assert.Equal(testCategoryList, resultCategories);
    }

    [Fact]
    public void Test_AddTask_AddsTaskToCategory()
    {
      //Arrange
      Category testCategory = new Category("Household chores");
      testCategory.Save();
      DateTime testTime = new DateTime(2016, 1, 1);

      Task testTask = new Task("Mow the lawn",testTime);
      testTask.Save();

      Task testTask2 = new Task("Water the garden",testTime);
      testTask2.Save();

      //Act
      testCategory.AddTask(testTask);
      testCategory.AddTask(testTask2);

      List<Task> result = testCategory.GetTasks();
      List<Task> testList = new List<Task> {testTask, testTask2};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_GetTasks_ReturnAllCategoryTasks()
    {
      Category testCategory = new Category("Househould chores");
      testCategory.Save();
      DateTime testTime = new DateTime(2016, 1, 1);

      Task testTask1= new Task("Mow the lawn",testTime);
      testTask1.Save();

      Task testTask2 = new Task("Buy plane ticket",testTime);
      testTask2.Save();

      testCategory.AddTask(testTask1);
      List<Task> savedTasks = testCategory.GetTasks();
      List<Task> testList= new List<Task>{testTask1};
      Assert.Equal(testList,savedTasks);
    }

    [Fact]
    public void Test_Delete_DeletesCategoryAssociationsFromDatabase()
    {
      //Arrange
      DateTime testTime = new DateTime(2016, 1, 1);

      Task testTask = new Task("Mow the lawn",testTime);
      testTask.Save();

      string testName = "Home stuff";
      Category testCategory = new Category(testName);
      testCategory.Save();

      //Act
      testCategory.AddTask(testTask);
      testCategory.Delete();

      List<Category> resultTaskCategories = testTask.GetCategories();
      List<Category> testTaskCategories = new List<Category> {};
      List<Task> resultsTasks= Task.GetAll();
      List<Task> testTasks= new List<Task>{testTask};

      //Assert
      Assert.Equal(testTaskCategories, resultTaskCategories);
      Assert.Equal(testTasks, resultsTasks);

    }

    public void Dispose()
    {
      Task.DeleteAll();
      Category.DeleteAll();
    }
  }
}
