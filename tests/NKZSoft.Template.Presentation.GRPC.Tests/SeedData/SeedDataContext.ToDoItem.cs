﻿namespace NKZSoft.Template.Presentation.Grpc.Tests.SeedData;

internal sealed partial class SeedDataContext
{
    public static IEnumerable<ToDoItem> ToDoItems
    {
        get
        {
            yield return new ToDoItem("TestItem_1", "Test Description_1");
            yield return new ToDoItem("TestItem_2", "Test Description_2");
            yield return new ToDoItem("TestItem_3", "Test Description_3");
            yield return new ToDoItem("TestItem_4", "Test Description_4");
        }
    }
}
