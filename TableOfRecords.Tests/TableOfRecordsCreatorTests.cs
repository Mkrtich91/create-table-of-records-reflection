using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using NUnit.Framework;
using TableOfRecords.Tests.UserProfiles;

namespace TableOfRecords.Tests
{
    [TestFixture]
    public class TableOfRecordsCreatorTests
    {
        private const string Path = "Table.txt";

        [OneTimeSetUp]
        public void Setup()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        [Test]
        public void WriteTable_With_ProfileBasic_Class()
        {
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            TableOfRecordsCreator.WriteTable(TestCasesDataSource.ProfileBasic, stringWriter);
            var streamWriter = new StreamWriter(Path);
            TableOfRecordsCreator.WriteTable(TestCasesDataSource.ProfileBasic, streamWriter);
            streamWriter.Close();
            var streamReader = new StreamReader(Path);
            Assert.AreEqual(stringWriter.ToString(), streamReader.ReadToEnd());
            streamReader.Close();
            File.Delete(Path);
        }

        [Test]
        public void WriteTable_With_ProfileExtended_Class()
        {
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            TableOfRecordsCreator.WriteTable(TestCasesDataSource.ProfileExtended, stringWriter);
            var streamWriter = new StreamWriter(Path);
            TableOfRecordsCreator.WriteTable(TestCasesDataSource.ProfileExtended, streamWriter);
            streamWriter.Close();
            var streamReader = new StreamReader(Path);
            Assert.AreEqual(stringWriter.ToString(), streamReader.ReadToEnd());
            streamReader.Close();
            File.Delete(Path);
        }

        [Test]
        public void WriteTable_With_ProfileShort_Class()
        {
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            TableOfRecordsCreator.WriteTable(TestCasesDataSource.ProfileShort, stringWriter);
            var streamWriter = new StreamWriter(Path);
            TableOfRecordsCreator.WriteTable(TestCasesDataSource.ProfileShort, streamWriter);
            streamWriter.Close();
            var streamReader = new StreamReader(Path);
            Assert.AreEqual(stringWriter.ToString(), streamReader.ReadToEnd());
            streamReader.Close();
            File.Delete(Path);
        }

        [Test]
        public void WriteTable_With_ProfileShort_And_Console()
        {
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            TableOfRecordsCreator.WriteTable(TestCasesDataSource.ProfileShortCollection, stringWriter);
            Assert.AreEqual(TestCasesDataSource.ProfileShortTable, stringWriter.ToString());
        }

        [Test]
        public void WriteTable_With_ProfileBasic_And_Console()
        {
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            TableOfRecordsCreator.WriteTable(TestCasesDataSource.ProfileBasicCollection, stringWriter);
            Assert.AreEqual(TestCasesDataSource.ProfileBasicTable, stringWriter.ToString());
        }

        [Test]
        public void WriteTable_With_ProfileExtended_And_Console()
        {
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            TableOfRecordsCreator.WriteTable(TestCasesDataSource.ProfileExtendedCollection, stringWriter);
            Assert.AreEqual(TestCasesDataSource.ProfileExtendedTable, stringWriter.ToString());
        }

        [Test]
        public void WriteTable_If_Collection_Is_Null_Throw_ArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() =>
                TableOfRecordsCreator.WriteTable<ProfileBasic>(null!, Console.Out));

        [Test]
        public void WriteTable_If_Collection_Is_Empty_Throw_ArgumentException() =>
            Assert.Throws<ArgumentException>(() =>
                TableOfRecordsCreator.WriteTable(Array.Empty<ICollection<ProfileBasic>>(), Console.Out));

        [Test]
        public void WriteTable_If_TextWriter_Is_Null_Throw_ArgumentNullException()
            => Assert.Throws<ArgumentNullException>(() =>
                TableOfRecordsCreator.WriteTable(TestCasesDataSource.ProfileBasic, null));
    }
}
