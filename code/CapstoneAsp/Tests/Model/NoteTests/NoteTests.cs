using System.Diagnostics.CodeAnalysis;
using CapstoneASP.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneASP.Tests.Model.NoteTests;

[TestClass]
[ExcludeFromCodeCoverage]
public class NoteTests
{
    #region Methods

    [TestMethod]
    [ExcludeFromCodeCoverage]
    public void Equals_SameNoteId_ReturnsTrue()
    {
        // Arrange
        var note1 = new Note { Note_Id = 1 };
        var note2 = new Note { Note_Id = 1 };

        // Act
        var result = note1.Equals(note2);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    [ExcludeFromCodeCoverage]
    public void Equals_DifferentNoteId_ReturnsFalse()
    {
        // Arrange
        var note1 = new Note { Note_Id = 1 };
        var note2 = new Note { Note_Id = 2 };

        // Act
        var result = note1.Equals(note2);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    [ExcludeFromCodeCoverage]
    public void GetHashCode_ReturnsHashCode()
    {
        // Arrange
        var note = new Note { Note_Id = 1 };

        // Act
        var hashCode = note.GetHashCode();

        // Assert
        Assert.AreEqual(note.Note_Id.GetHashCode(), hashCode);
    }

    [TestMethod]
    [ExcludeFromCodeCoverage]
    public void Equals_CompareWithNull_ReturnsFalse()
    {
        // Arrange
        var note = new Note { Note_Id = 1 };

        // Act
        var result = note.Equals(null);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    [ExcludeFromCodeCoverage]
    public void Equals_CompareWithDifferentType_ReturnsFalse()
    {
        // Arrange
        var note = new Note { Note_Id = 1 };

        // Act
        var result = note.Equals("not a Note");

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    [ExcludeFromCodeCoverage]
    public void TestCreatingValidNote()
    {
        var note = new Note
        {
            Content = "",
            Note_Id = 1,
            Source_Id = 1,
            Username = "Test user"
        };
        Assert.AreEqual("", note.Content);
        Assert.AreEqual(1, note.Note_Id);
        Assert.AreEqual(1, note.Source_Id);
        Assert.AreEqual("Test user", note.Username);
    }

    #endregion
}