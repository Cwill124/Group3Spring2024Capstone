using System.Diagnostics.CodeAnalysis;
using CapstoneASP.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CapstoneASP.Tests.Model.NoteTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class NoteTests
    {
        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void Equals_SameNoteId_ReturnsTrue()
        {
            // Arrange
            var note1 = new Note { NoteId = 1 };
            var note2 = new Note { NoteId = 1 };

            // Act
            bool result = note1.Equals(note2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void Equals_DifferentNoteId_ReturnsFalse()
        {
            // Arrange
            var note1 = new Note { NoteId = 1 };
            var note2 = new Note { NoteId = 2 };

            // Act
            bool result = note1.Equals(note2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void GetHashCode_ReturnsHashCode()
        {
            // Arrange
            var note = new Note { NoteId = 1 };

            // Act
            int hashCode = note.GetHashCode();

            // Assert
            Assert.AreEqual(note.NoteId.GetHashCode(), hashCode);
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void Equals_CompareWithNull_ReturnsFalse()
        {
            // Arrange
            var note = new Note { NoteId = 1 };

            // Act
            bool result = note.Equals(null);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void Equals_CompareWithDifferentType_ReturnsFalse()
        {
            // Arrange
            var note = new Note { NoteId = 1 };

            // Act
            bool result = note.Equals("not a Note");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void TestCreatingValidNote()
        {
            var note = new Note()
            {
                Content = "",
                NoteId = 1,
                SourceId = 1,
                Username = "Test user"
            };
            Assert.AreEqual("", note.Content);
            Assert.AreEqual(1, note.NoteId);
            Assert.AreEqual(1, note.SourceId);
            Assert.AreEqual("Test user", note.Username);

        }

    }
}

